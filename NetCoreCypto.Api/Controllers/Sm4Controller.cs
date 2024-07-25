using Microsoft.AspNetCore.Mvc;
using NetCoreCrypto.SM4;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace NetCoreCypto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sm4Controller : ControllerBase
    {
        private readonly ISm4EncryptionService _encryptionService;

        public Sm4Controller(ISm4EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public void Sm4Test()
        {
            var str = "hello word!";
            var key = "6666666666666666";
            var iv = "8888888888888888";

            byte[] bytes = _encryptionService.Encrypt(Encoding.UTF8.GetBytes(str), Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv), Sm4EncryptionNames.ModeCBC);
            string encryptStr = Hex.ToHexString(bytes, true);

            byte[] encryptBytes = Hex.DecodeStrict(encryptStr);
            byte[] decryptBytes = _encryptionService.Decrypt(encryptBytes, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv), Sm4EncryptionNames.ModeCBC);
            string decryptStr = Encoding.UTF8.GetString(decryptBytes);
        }
    }
}
