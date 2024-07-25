using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreCrypto.SM3;
using NetCoreCrypto.SM4;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCrypto.SM4.Tests
{
    [TestClass()]
    public class Sm4EncryptionServiceTests
    {
        private readonly ServiceProvider _serviceProvider;

        public Sm4EncryptionServiceTests()
        {
            var services = new ServiceCollection();

            services.AddTransient<ISm4EncryptionService, Sm4EncryptionService>();

            // Sm4EncryptionOptions 配置
            services.Configure<Sm4EncryptionOptions>(options =>
            {
                // 16位
                options.DefaultIv = Encoding.UTF8.GetBytes("8888888888888888");
                options.DefaultMode = Sm4EncryptionNames.ModeECB;
                options.DefaultPadding = Sm4EncryptionNames.NoPadding;
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod()]
        public void Sm4Test()
        {
            var _encryptionService = _serviceProvider.GetRequiredService<ISm4EncryptionService>();

            var str = "hello word!";
            var key = "6666666666666666";
            var iv = "8888888888888888";

            byte[] bytes = _encryptionService.Encrypt(Encoding.UTF8.GetBytes(str), Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv), Sm4EncryptionNames.ModeCBC);
            string encryptStr = Hex.ToHexString(bytes, true);

            byte[] encryptBytes = Hex.DecodeStrict(encryptStr);
            byte[] decryptBytes = _encryptionService.Decrypt(encryptBytes, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv), Sm4EncryptionNames.ModeCBC);
            string decryptStr = Encoding.UTF8.GetString(decryptBytes);

            Assert.AreEqual(str, decryptStr);
        }
    }
}