using Microsoft.AspNetCore.Mvc;
using NetCoreCrypto.SM2;

namespace NetCoreCypto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sm2Controller : ControllerBase
    {
        private readonly ISm2EncryptionService _encryptionService;

        public Sm2Controller(ISm2EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public void Sm2Test()
        {
            var asymmetricCipherKeyPair = _encryptionService.GenerateSm2KeyPair();
            string pub = asymmetricCipherKeyPair.ExportPublicKey();
            string pri = asymmetricCipherKeyPair.ExportPrivateKey();

            string str = "hello word!";
            string enctyptStr = _encryptionService.Encrypt(pub, str);
            string decryptStr = _encryptionService.Decrypt(pri, enctyptStr);

            string sign = _encryptionService.Sign(pri, str);
            bool isSign = _encryptionService.VerifySign(pub, str, sign);

        }
    }
}
