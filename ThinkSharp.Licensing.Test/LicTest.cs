// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkSharp.Licensing;
using ThinkSharp.Licensing.Signing.RSA;

namespace ThinkSharp.Licensing.Test
{
  [TestClass]
    public class LicTest
    {
        // Private
        internal static string PrivateKey = "BwIAAACkAABSU0EyAAQAAAEAAQA9D9kxlsh1N5KPmhOsJj/QM9iZVaho5OzEG0gyO8s0Giycx7ttegjugLE1NY7Gw5FPJvSqrlRiBp9iNCsD9/NUJIa65mwfTsShzoce+v5tRLJrd4osZZ3WFA/e4oSk9BgCJNUWIShj1HKD4Lk1YqGWtaMZnx/uNLe8QZ4FGYkKvOWDl4FaLViZBbGfLBxMoMpPGQVmSbJtlOoqjyQr0J9stuuJCs564uTzXqJU9/ytInlFYGEDOpYanlkio4x38Px5WAF4+EPvplW6IszdwsR+Sd6hkSqwu8IPzkZwU6PsyvPF2tLQgomB4LVh/6gJcDpNCXJLWXm4GG+YuHpiCFG+6fPFbd0vcDc5Y4ByAtUADFQ0q2kdI+8K5znNJBd9xeuTF9mJLFKbvENJ58F+DPCtWLEWf5tYZXicZUfTa/tnmzFQKx1lc3wHYh5DyQttkmvsN4bXrp0whYU1S8eiI22H5meA5C6CiJZKSXWEGAAA2bpSgRt5ltS1WIR+wVGZ5iFkwnrkQldBZxecWCpj5HQexyDPcpsJipSotRllkNP8K8TubP8YafQGntQjeozZ4mOz2+V3f7GuC5DX229fGIv54ULq7ftWS3sqLSYzf1DJbN//bQkZQCAP5s5UXlx3n6G68cmkTaSE4ZP2slleqF6CEAaMS99jh2deYslQu1Jp395XkYoqnkzWiAIuoIYIaUxMn5+HWdqS6gqyGVHdbnWvlncdcoest/0waxkeSC86QqZ77QFzLaKSfDnYpHZ2t1o=";

        // Public
        internal static string PublicKey = "BgIAAACkAABSU0ExAAQAAAEAAQA9D9kxlsh1N5KPmhOsJj/QM9iZVaho5OzEG0gyO8s0Giycx7ttegjugLE1NY7Gw5FPJvSqrlRiBp9iNCsD9/NUJIa65mwfTsShzoce+v5tRLJrd4osZZ3WFA/e4oSk9BgCJNUWIShj1HKD4Lk1YqGWtaMZnx/uNLe8QZ4FGYkKvA==";
        
    }
}
