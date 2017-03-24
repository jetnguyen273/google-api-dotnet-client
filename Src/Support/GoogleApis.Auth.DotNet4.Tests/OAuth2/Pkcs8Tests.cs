﻿/*
Copyright 2016 Google Inc

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using NUnit.Framework;
using System.Linq;
using System.Security.Cryptography;

namespace Google.Apis.Auth.OAuth2
{
    [TestFixture]
    class Pkcs8Tests
    {
        // This is the private key from a service credential, as downloaded from the Cloud Console on 2016-12-08
        string testPrivateKey = @"
-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDQvitimRPWE2YM
tOADu3jb89UQmqZz8ao6NFoAjYiYDdg/6UBUTscQwGb6rIvllMtcT6BdS4rMKUwv
kSQFbO14gi7FiZBj4eJKveObVnt9RPxqT1txBieEPFtgJalh0GpyCo5GUmadgqe+
89ZF+VQBFE2hMoZhBny/FDba2/qDIk1GmksdXjC67ZI0EYRCGI71eXjOpQX/wOJs
tosMQRSTHjPKhZKvZTtjksMRWm8SlQoyORZKXsV6OGKztOieIvncKoX3lrkSupax
J+v0yYQJIF2uTFDbbCDL86bgUJ7TyZJ9X1AvrnHGNWp+QzQBDbTCMpZCuY81ARdu
BHeacqR3AgMBAAECggEAK7Cx+fgaO8Nhp6UwAff6KudVIB2OW1QokfglIlp9TX4Q
VggnC75VUf9DTpJQ0aOcEN0lroFCMssuBAK37F7JMWDmEzhgvVco+wXVnsyyGh0X
S9UCSZzFJptPcMdRNYTe0rG856EVk0AmhgQZRBoUaAls2iFuGN63u3KqrJJAU7O4
MPofQizwNcMmiDqRV0rnU6HgmIRQ64hMMh1bnnw4I35esOXjO1O2PiwyyK0jHuVz
eBNeD0wEXNLIP4CqDsQ0DKyBPKXZW4bmFnluq4s+VvgPMczUli7Ph0bM7G1oR0LF
nZk8tSO9cZ4lLcZpY5mHg7lHzSE6oPAxrkPYoa+6IQKBgQD6I7KaUcmkQj+vOQ18
sZCA28tXqjW2cGB4ThqG2uYeHK8khhjh/Pj07TFr8Xjpz1gNr1NNBJhR9Qs/dO9N
WCXS6s0fsVz/gm8N36w7kcBe+Uo43UIt3U6xvZVG4eJYWe2B5+0sZeXzMfzGhwug
VkinOJ0CoVpGFTByPrn17O9T8QKBgQDVoi5AO7jvL3nxLQyfXy6XhkHvmnrRU5hV
eHrgO2+0TsjAHNRhGGrKTMDqCrMvJqxXbXAwNIKxPdzGCXjam4QzaXgp8+bJWHNe
UNRfbWUxVo/Mzfp4owN6oCOVN2cTrnz/doFhuT1LjDj2KK5+hY6qtleH+h00XjCi
0xrTGCpG5wKBgDy0pya+jKI5lb0Pqo9FhJ1ROkM3QrvZAACSa+uoekp6iaLijG1j
+INwgRsCSmbr9CG2GBBL+i+Buc4Xse/iYaOTal6zq68y14LVcrYuRDKIa5PrVqFM
4UlPikfekBEDGhn50FyDClCAJCmGIrMx3YX/vlMiF4eEovJG+NiPPPHxAoGBANQq
qKJ0bbtmPEYQxot1HTGxTcSneDhyPEUOTYJqpQq+f9OEDkyL0msthR4rGD/Iubpu
XtARJobeeGdZuuPpNYdVxNhteZQXuyQ9RF2tqKUyYcg1/P5YbzkW15/3EPDUByIz
UFV8geqIzX1zc7EF9WWHiDDsbpq2vLjIzcg+JKabAoGAI7kuoGBdvIHne3Y3tucQ
JzcnYI3bppbhyn1dmm8jsaaWrtBvFqCb1y1MjaoL4lNi+brdd6bpV5u447GBQfG8
o3L1luXTksRPmwh5NbhXWl/ieiWc6WQL6sr8Q+vSYDKAo95zdhWJvWs+/v7NfWoF
lK1DcBvq+IFLucBdi0/9hXE=
-----END PRIVATE KEY-----";

        private string ToHex(byte[] bs) => bs.Aggregate("", (acc, b) => acc + b.ToString("X2"));

        [Test]
        public void Pkcs8Decoder()
        {
            RSAParameters ps = Pkcs8.DecodeRsaParameters(testPrivateKey);
            Assert.That(ToHex(ps.Modulus), Is.EqualTo(
                "D0BE2B629913D613660CB4E003BB78DBF3D5109AA673F1AA3A345A008D88980DD83FE940544EC710C066FAAC8BE594CB5C4FA05D4B8ACC294C2F9124056CED78822EC5899063E1E24ABDE39B567B7D44FC6A4F5B710627843C5B6025A961D06A720A8E4652669D82A7BEF3D645F95401144DA1328661067CBF1436DADBFA83224D469A4B1D5E30BAED9234118442188EF57978CEA505FFC0E26CB68B0C4114931E33CA8592AF653B6392C3115A6F12950A3239164A5EC57A3862B3B4E89E22F9DC2A85F796B912BA96B127EBF4C98409205DAE4C50DB6C20CBF3A6E0509ED3C9927D5F502FAE71C6356A7E4334010DB4C2329642B98F3501176E04779A72A477"));
            Assert.That(ToHex(ps.Exponent), Is.EqualTo(
                "010001"));
            Assert.That(ToHex(ps.D), Is.EqualTo(
                "2BB0B1F9F81A3BC361A7A53001F7FA2AE755201D8E5B542891F825225A7D4D7E105608270BBE5551FF434E9250D1A39C10DD25AE814232CB2E0402B7EC5EC93160E6133860BD5728FB05D59ECCB21A1D174BD502499CC5269B4F70C7513584DED2B1BCE7A115934026860419441A1468096CDA216E18DEB7BB72AAAC924053B3B830FA1F422CF035C326883A91574AE753A1E0988450EB884C321D5B9E7C38237E5EB0E5E33B53B63E2C32C8AD231EE57378135E0F4C045CD2C83F80AA0EC4340CAC813CA5D95B86E616796EAB8B3E56F80F31CCD4962ECF8746CCEC6D684742C59D993CB523BD719E252DC66963998783B947CD213AA0F031AE43D8A1AFBA21"));
            Assert.That(ToHex(ps.P), Is.EqualTo(
                "FA23B29A51C9A4423FAF390D7CB19080DBCB57AA35B67060784E1A86DAE61E1CAF248618E1FCF8F4ED316BF178E9CF580DAF534D049851F50B3F74EF4D5825D2EACD1FB15CFF826F0DDFAC3B91C05EF94A38DD422DDD4EB1BD9546E1E25859ED81E7ED2C65E5F331FCC6870BA05648A7389D02A15A461530723EB9F5ECEF53F1"));
            Assert.That(ToHex(ps.Q), Is.EqualTo(
                "D5A22E403BB8EF2F79F12D0C9F5F2E978641EF9A7AD1539855787AE03B6FB44EC8C01CD461186ACA4CC0EA0AB32F26AC576D70303482B13DDCC60978DA9B8433697829F3E6C958735E50D45F6D6531568FCCCDFA78A3037AA02395376713AE7CFF768161B93D4B8C38F628AE7E858EAAB65787FA1D345E30A2D31AD3182A46E7"));
            Assert.That(ToHex(ps.DP), Is.EqualTo(
                "3CB4A726BE8CA23995BD0FAA8F45849D513A433742BBD90000926BEBA87A4A7A89A2E28C6D63F88370811B024A66EBF421B618104BFA2F81B9CE17B1EFE261A3936A5EB3ABAF32D782D572B62E4432886B93EB56A14CE1494F8A47DE9011031A19F9D05C830A508024298622B331DD85FFBE5322178784A2F246F8D88F3CF1F1"));
            Assert.That(ToHex(ps.DQ), Is.EqualTo(
                "D42AA8A2746DBB663C4610C68B751D31B14DC4A77838723C450E4D826AA50ABE7FD3840E4C8BD26B2D851E2B183FC8B9BA6E5ED0112686DE786759BAE3E9358755C4D86D799417BB243D445DADA8A53261C835FCFE586F3916D79FF710F0D407223350557C81EA88CD7D7373B105F565878830EC6E9AB6BCB8C8CDC83E24A69B"));
            Assert.That(ToHex(ps.InverseQ), Is.EqualTo(
                "23B92EA0605DBC81E77B7637B6E710273727608DDBA696E1CA7D5D9A6F23B1A696AED06F16A09BD72D4C8DAA0BE25362F9BADD77A6E9579BB8E3B18141F1BCA372F596E5D392C44F9B087935B8575A5FE27A259CE9640BEACAFC43EBD2603280A3DE73761589BD6B3EFEFECD7D6A0594AD43701BEAF8814BB9C05D8B4FFD8571"));
        }

        [Test]
        public void RsaImportExport()
        {
            RSAParameters ps0 = Pkcs8.DecodeRsaParameters(testPrivateKey);
            RSA rsa = RSA.Create();
            rsa.ImportParameters(ps0);
            RSAParameters ps = rsa.ExportParameters(true);
            Assert.That(ToHex(ps.Modulus), Is.EqualTo(
                "D0BE2B629913D613660CB4E003BB78DBF3D5109AA673F1AA3A345A008D88980DD83FE940544EC710C066FAAC8BE594CB5C4FA05D4B8ACC294C2F9124056CED78822EC5899063E1E24ABDE39B567B7D44FC6A4F5B710627843C5B6025A961D06A720A8E4652669D82A7BEF3D645F95401144DA1328661067CBF1436DADBFA83224D469A4B1D5E30BAED9234118442188EF57978CEA505FFC0E26CB68B0C4114931E33CA8592AF653B6392C3115A6F12950A3239164A5EC57A3862B3B4E89E22F9DC2A85F796B912BA96B127EBF4C98409205DAE4C50DB6C20CBF3A6E0509ED3C9927D5F502FAE71C6356A7E4334010DB4C2329642B98F3501176E04779A72A477"));
            Assert.That(ToHex(ps.Exponent), Is.EqualTo(
                "010001"));
            Assert.That(ToHex(ps.D), Is.EqualTo(
                "2BB0B1F9F81A3BC361A7A53001F7FA2AE755201D8E5B542891F825225A7D4D7E105608270BBE5551FF434E9250D1A39C10DD25AE814232CB2E0402B7EC5EC93160E6133860BD5728FB05D59ECCB21A1D174BD502499CC5269B4F70C7513584DED2B1BCE7A115934026860419441A1468096CDA216E18DEB7BB72AAAC924053B3B830FA1F422CF035C326883A91574AE753A1E0988450EB884C321D5B9E7C38237E5EB0E5E33B53B63E2C32C8AD231EE57378135E0F4C045CD2C83F80AA0EC4340CAC813CA5D95B86E616796EAB8B3E56F80F31CCD4962ECF8746CCEC6D684742C59D993CB523BD719E252DC66963998783B947CD213AA0F031AE43D8A1AFBA21"));
            Assert.That(ToHex(ps.P), Is.EqualTo(
                "FA23B29A51C9A4423FAF390D7CB19080DBCB57AA35B67060784E1A86DAE61E1CAF248618E1FCF8F4ED316BF178E9CF580DAF534D049851F50B3F74EF4D5825D2EACD1FB15CFF826F0DDFAC3B91C05EF94A38DD422DDD4EB1BD9546E1E25859ED81E7ED2C65E5F331FCC6870BA05648A7389D02A15A461530723EB9F5ECEF53F1"));
            Assert.That(ToHex(ps.Q), Is.EqualTo(
                "D5A22E403BB8EF2F79F12D0C9F5F2E978641EF9A7AD1539855787AE03B6FB44EC8C01CD461186ACA4CC0EA0AB32F26AC576D70303482B13DDCC60978DA9B8433697829F3E6C958735E50D45F6D6531568FCCCDFA78A3037AA02395376713AE7CFF768161B93D4B8C38F628AE7E858EAAB65787FA1D345E30A2D31AD3182A46E7"));
            Assert.That(ToHex(ps.DP), Is.EqualTo(
                "3CB4A726BE8CA23995BD0FAA8F45849D513A433742BBD90000926BEBA87A4A7A89A2E28C6D63F88370811B024A66EBF421B618104BFA2F81B9CE17B1EFE261A3936A5EB3ABAF32D782D572B62E4432886B93EB56A14CE1494F8A47DE9011031A19F9D05C830A508024298622B331DD85FFBE5322178784A2F246F8D88F3CF1F1"));
            Assert.That(ToHex(ps.DQ), Is.EqualTo(
                "D42AA8A2746DBB663C4610C68B751D31B14DC4A77838723C450E4D826AA50ABE7FD3840E4C8BD26B2D851E2B183FC8B9BA6E5ED0112686DE786759BAE3E9358755C4D86D799417BB243D445DADA8A53261C835FCFE586F3916D79FF710F0D407223350557C81EA88CD7D7373B105F565878830EC6E9AB6BCB8C8CDC83E24A69B"));
            Assert.That(ToHex(ps.InverseQ), Is.EqualTo(
                "23B92EA0605DBC81E77B7637B6E710273727608DDBA696E1CA7D5D9A6F23B1A696AED06F16A09BD72D4C8DAA0BE25362F9BADD77A6E9579BB8E3B18141F1BCA372F596E5D392C44F9B087935B8575A5FE27A259CE9640BEACAFC43EBD2603280A3DE73761589BD6B3EFEFECD7D6A0594AD43701BEAF8814BB9C05D8B4FFD8571"));
        }

#if !NETSTANDARD
        [Test]
        public void RsaFuzzTest()
        {
            // Create many RSA keys, encode them in PKCS8, verify the Pkcs8 class can decode them correctly.
            // This test does take a few (possibly 10s of) seconds, longer than most unit tests.
            for (int i = 0; i < 1000; i++)
            {
                // This SecureRandom constructor is deprecated,
                // but is the easiest way to create a deterministic SecureRandom.
#pragma warning disable CS0618
                var rnd = new Org.BouncyCastle.Security.SecureRandom(new byte[] { (byte)(i & 0xff), (byte)((i >> 8) & 0xff) });
#pragma warning restore CS0618
                var rsa = new Org.BouncyCastle.Crypto.Generators.RsaKeyPairGenerator();
                // 384 is the shortest valid key length. Use this for speed.
                rsa.Init(new Org.BouncyCastle.Crypto.KeyGenerationParameters(rnd, 384));
                var keys = rsa.GenerateKeyPair();
                var pkcs8Generator = new Org.BouncyCastle.OpenSsl.Pkcs8Generator(keys.Private);
                var pem = pkcs8Generator.Generate();
                var ms = new System.IO.MemoryStream();
                var stWriter = new System.IO.StreamWriter(ms);
                var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(stWriter);
                pemWriter.WriteObject(pem);
                stWriter.Close();
                var pkcs8 = System.Text.Encoding.ASCII.GetString(ms.ToArray());
                var rsaParameters = Pkcs8.DecodeRsaParameters(pkcs8);
                var key = RSA.Create();
                try
                {
                    // Test that the parameters can be imported.
                    // Throws CryptographicException if the rsaParameters is invalid
                    key.ImportParameters(rsaParameters);
                }
                catch (CryptographicException e)
                {
                    // Fails in iteration 8 without the Pkcs8 fix in PR#937
                    Assert.Fail($"Failed in iteration {i}: {e}");
                }
                // Check that all the parameters exported are equal to the originally created parameters
                var exportedParams = key.ExportParameters(true);
                var privateKey = (Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters)keys.Private;
                Assert.That(Pkcs8.TrimLeadingZeroes(exportedParams.P, false), Is.EqualTo(privateKey.P.ToByteArrayUnsigned()));
                Assert.That(Pkcs8.TrimLeadingZeroes(exportedParams.Q, false), Is.EqualTo(privateKey.Q.ToByteArrayUnsigned()));
                Assert.That(Pkcs8.TrimLeadingZeroes(exportedParams.DP, false), Is.EqualTo(privateKey.DP.ToByteArrayUnsigned()));
                Assert.That(Pkcs8.TrimLeadingZeroes(exportedParams.DQ, false), Is.EqualTo(privateKey.DQ.ToByteArrayUnsigned()));
                var publicKey = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)keys.Public;
                Assert.That(Pkcs8.TrimLeadingZeroes(exportedParams.Exponent, false), Is.EqualTo(publicKey.Exponent.ToByteArrayUnsigned()));
                Assert.That(Pkcs8.TrimLeadingZeroes(exportedParams.Modulus, false), Is.EqualTo(publicKey.Modulus.ToByteArrayUnsigned()));
            }
        }
#endif

        [Test]
        public void TrimLeadingZeroes()
        {
            Assert.That(
                Pkcs8.TrimLeadingZeroes(new byte[] { 0, 1 }, alignTo8Bytes: false),
                Is.EqualTo(new byte[] { 1 }));
            Assert.That(
                Pkcs8.TrimLeadingZeroes(new byte[] { 0, 1 }, alignTo8Bytes: true),
                Is.EqualTo(new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }));
            Assert.That(
                Pkcs8.TrimLeadingZeroes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, alignTo8Bytes: false),
                Is.EqualTo(new byte[] { 1 }));
            Assert.That(
                Pkcs8.TrimLeadingZeroes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, alignTo8Bytes: true),
                Is.EqualTo(new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }));
        }
    }
}
