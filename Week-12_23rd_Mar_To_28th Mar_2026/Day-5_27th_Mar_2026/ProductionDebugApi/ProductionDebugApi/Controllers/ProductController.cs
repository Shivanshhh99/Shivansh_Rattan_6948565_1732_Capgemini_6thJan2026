using Microsoft.AspNetCore.Mvc;

namespace ProductionDebuggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("fast")]
        public IActionResult FastApi()
        {
            return Ok(new
            {
                Message = "Fast API executed successfully"
            });
        }

        [HttpGet("slow")]
        public async Task<IActionResult> SlowApi()
        {
            await Task.Delay(5000); // 5 seconds delay

            return Ok(new
            {
                Message = "Slow API executed successfully"
            });
        }

        [HttpGet("error")]
        public IActionResult ErrorApi()
        {
            string? value = null;

            // This will throw exception
            int length = value.Length;

            return Ok(new
            {
                Message = "This will never execute",
                Length = length
            });
        }
    }
}