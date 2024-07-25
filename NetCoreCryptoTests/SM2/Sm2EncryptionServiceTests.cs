using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreCrypto.RSA;
using NetCoreCrypto.SM2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCrypto.SM2.Tests
{
    [TestClass()]
    public class Sm2EncryptionServiceTests
    {
        private readonly ServiceProvider _serviceProvider;

        public Sm2EncryptionServiceTests()
        {
            var services = new ServiceCollection();

            services.AddTransient<ISm2EncryptionService, Sm2EncryptionService>();

            // Sm2EncryptionOptions 配置
            services.Configure<Sm2EncryptionOptions>(options =>
            {
                options.DefaultCurve = Sm2EncryptionNames.CurveSm2p256v1;
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod()]
        public void Sm2Test()
        {
            var _encryptionService = _serviceProvider.GetRequiredService<ISm2EncryptionService>();

            var asymmetricCipherKeyPair = _encryptionService.GenerateSm2KeyPair();
            string pub = asymmetricCipherKeyPair.ExportPublicKey();
            string pri = asymmetricCipherKeyPair.ExportPrivateKey();

            string str = "hello word!";
            string enctyptStr = _encryptionService.Encrypt(pub, str);
            string decryptStr = _encryptionService.Decrypt(pri, enctyptStr);

            Assert.AreEqual(str, decryptStr);

            string sign = _encryptionService.Sign(pri, str);
            bool isSign = _encryptionService.VerifySign(pub, str, sign);

            Assert.IsTrue(isSign);
        }
    }
}