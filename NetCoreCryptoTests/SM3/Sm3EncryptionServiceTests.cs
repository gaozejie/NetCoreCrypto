using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreCrypto.SM2;
using NetCoreCrypto.SM3;
using NetCoreCrypto.SM4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCrypto.SM3.Tests
{
    [TestClass()]
    public class Sm3EncryptionServiceTests
    {
        private readonly ServiceProvider _serviceProvider;
        public Sm3EncryptionServiceTests()
        {
            var services = new ServiceCollection();

            services.AddTransient<ISm3EncryptionService, Sm3EncryptionService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod()]
        public void Sm3Test()
        {
            var _encryptionService = _serviceProvider.GetRequiredService<ISm3EncryptionService>();

            var str = "hello word!";
            string hash = _encryptionService.GetHash(str);

            Assert.AreEqual(hash, "54c71ebfd39a6aee462ef495262b91cdc8fb056d4ab5142e9b0a0279bf743cd6");
        }
    }
}