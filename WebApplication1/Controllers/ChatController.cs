using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using OllamaSharp.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly OllamaApiClient _client;

        public ChatController()
        {
            _client = new OllamaApiClient("http://localhost:11434");
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string prompt)
        {
            // The GenerateAsync method returns IAsyncEnumerable<GenerateResponseStream?>
            // We need to enumerate it and collect the response.
            var request = new GenerateRequest
            {
                Prompt = prompt,
                Model = "llama3"
                // You may need to set other properties if required by your model
            };

            string result = "";
            await foreach (var response in _client.GenerateAsync(request))
            {
                if (response != null && response.Response != null)
                {
                    result += response.Response;
                }
            }
            return Ok(result);
        }

    }
}
