using Microsoft.AspNetCore.Mvc;
using NetCoreCrypto.RSA;

namespace NetCoreCypto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSAController : ControllerBase
    {
        private readonly IRSAEncryptionService _encryptionService;

        public RSAController(IRSAEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public void RsaTest()
        {
            var asymmetricCipherKeyPair = _encryptionService.GenerateRSAKeyPair();
            string pub = asymmetricCipherKeyPair.ExportPublicKeyToPem();
            string pri = asymmetricCipherKeyPair.ExportPrivateKeyPkcs8ToPem();

            string str = "hellow word！";
            string encryptStr = _encryptionService.EncryptFromPem(pub, str);
            string d = _encryptionService.DecryptFromPkcs8Pem(pri, encryptStr);
        }
    }
}
