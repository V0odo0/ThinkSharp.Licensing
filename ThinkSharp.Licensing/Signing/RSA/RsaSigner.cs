﻿// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ThinkSharp.Licensing.Signing.RSA
{
    internal class RsaSigner : ISigner
    {
        private readonly RSACryptoServiceProvider myCryptoServiceProvider;
        private readonly HashAlgorithm myHashAlgo;

        private RsaSigner()
        {
            myCryptoServiceProvider = new RSACryptoServiceProvider();
            myHashAlgo = new SHA512CryptoServiceProvider();
        }

        public RsaSigner(RSAParameters rsaParameters)
            : this()
        {
            myCryptoServiceProvider.ImportParameters(rsaParameters);
        }

        public RsaSigner(string base64EncodedCsbBlobKey)
            : this(Convert.FromBase64String(base64EncodedCsbBlobKey))
        { }

        public RsaSigner(byte[] csbBlobKey)
            : this()
        {
            myCryptoServiceProvider.ImportCspBlob(csbBlobKey);
        }

        public bool Verify(string content, string signature)
        {
            var bytesContent = Encoding.UTF8.GetBytes(content);
            var bytesSignature = Convert.FromBase64String(signature);
            UnConfuse(bytesSignature);
            return myCryptoServiceProvider.VerifyData(bytesContent, myHashAlgo, bytesSignature);
        }

        private static void UnConfuse(byte[] bytes)
        {
            var confusingBytes = new byte[] { 2, 43, 2, 54, 199, 3, 43 };
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= confusingBytes[i % confusingBytes.Length];
        }
    }
}
