using System;
using System.Security.Cryptography;
using System.Text;
using ThinkSharp.Licensing.Signing;

namespace ThinkSharp.Licensing.Helper
{
    public static class ThinkSharpCrossPlatformHelpers
    {
        private class CrossPlatformRsaSigner : ISigner
        {
            private readonly RSACryptoServiceProvider _cryptoServiceProvider;
            private readonly HashAlgorithm _hashAlgorithm;

            private CrossPlatformRsaSigner()
            {
                _cryptoServiceProvider = new RSACryptoServiceProvider();
                _hashAlgorithm = SHA512.Create();
            }

            public CrossPlatformRsaSigner(RSAParameters rsaParameters)
                : this()
            {
                _cryptoServiceProvider.ImportParameters(rsaParameters);
            }

            public CrossPlatformRsaSigner(string base64EncodedCsbBlobKey)
                : this(Convert.FromBase64String(base64EncodedCsbBlobKey))
            {
            }

            public CrossPlatformRsaSigner(byte[] csbBlobKey)
                : this()
            {
                _cryptoServiceProvider.ImportCspBlob(csbBlobKey);
            }

            public bool Verify(string content, string signature)
            {
                content = content.Replace("\r\n", "\n");
                var bytesContent = Encoding.UTF8.GetBytes(content);
                var bytesSignature = Convert.FromBase64String(signature);
                UnConfuse(bytesSignature);
                return _cryptoServiceProvider.VerifyData(bytesContent, _hashAlgorithm, bytesSignature);
            }

            public string Sign(string content)
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                var signature = _cryptoServiceProvider.SignData(bytes, _hashAlgorithm);
                UnConfuse(signature);
                var base64Signing = Convert.ToBase64String(signature);

                return base64Signing;
            }

            private static void UnConfuse(byte[] bytes)
            {
                var confusingBytes = new byte[] { 2, 43, 2, 54, 199, 3, 43 };
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] ^= confusingBytes[i % confusingBytes.Length];
            }

        }

        public static string ReplaceLinuxNewLines(string licenseCode)
        {
            licenseCode = licenseCode.Unwrap();
            licenseCode = Dencrypt(licenseCode);

            var lines = licenseCode.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(Environment.NewLine, lines);

            string Dencrypt(string input)
            {
                var confusingBytes = new byte[] { 32, 45, 12, 43, 33, 1 };
                var bytes = Convert.FromBase64String(input);
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] ^= confusingBytes[i % confusingBytes.Length];
                return Encoding.UTF8.GetString(bytes);
            }
        }

        public static IVerifier_ApplicationCode WithCrossPlatformRsaSigner(this IVerifier_Signer signer, string base64EncodedCsbBlobKey)
        {
            return signer.WithSigner(new CrossPlatformRsaSigner(base64EncodedCsbBlobKey));
        }
    }
}
