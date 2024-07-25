using Microsoft.AspNetCore.Mvc;
using NetCoreCrypto.SM3;

namespace NetCoreCypto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sm3Controller
    {
        private readonly ISm3EncryptionService _encryptionService;

        public Sm3Controller(ISm3EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public void Sm3Test()
        {
            var str = "hello word!";
            string hash = _encryptionService.GetHash(str);

        }

    }
}
