using Microsoft.AspNetCore.Mvc;

namespace Laba5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {
        private readonly IRabbitMqService _mqService;
        public RabbitMqController(IRabbitMqService mqService)
        {
            _mqService = mqService;
        }
        [Route("[action]/{message}")]                      
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            _mqService.SendMessage(message);
            return Ok("Сообщение отправлено");
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendEmail(string message)
        {
            _mqService.SendEmail("grandluntik0@gmail.com", "New Message", message);
            return Ok("Сообщение отправлено");
        }
    }
}
