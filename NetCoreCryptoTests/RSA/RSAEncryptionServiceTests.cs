using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreCrypto.RSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCrypto.RSA.Tests
{
    [TestClass()]
    public class RSAEncryptionServiceTests
    {

        private readonly ServiceProvider _serviceProvider;

        public RSAEncryptionServiceTests()
        {
            var services = new ServiceCollection();

            services.AddTransient<IRSAEncryptionService, RSAEncryptionService>();

            _serviceProvider = services.BuildServiceProvider();

        }

        [TestMethod()]
        public void RSATest()
        {

            var _encryptionService = _serviceProvider.GetRequiredService<IRSAEncryptionService>();

            var asymmetricCipherKeyPair = _encryptionService.GenerateRSAKeyPair();
            string pub = asymmetricCipherKeyPair.ExportPublicKeyToPem();
            string pri = asymmetricCipherKeyPair.ExportPrivateKeyPkcs8ToPem();

            string str = "hellow word！";
            string encryptStr = _encryptionService.EncryptFromPem(pub, str);
            string decryptStr = _encryptionService.DecryptFromPkcs8Pem(pri, encryptStr);


            Assert.AreEqual(str, decryptStr);
        }
    }
}