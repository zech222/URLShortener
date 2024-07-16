using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using URLShortener.Models;
using URLShortener.Services;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : Controller
    {
        private readonly UrlShortener _urlShortener;

        public UrlController(UrlShortener urlShortener)
        {
            _urlShortener = urlShortener;
        }

        [HttpPost("ShortenUrl")]
        public async Task<IActionResult> ShortenUrl([FromBody] Urls url)
        {
            var shortUrl = await _urlShortener.ShortenUrlAsync(url.LongUrl);
            return Ok(shortUrl);
        }

        [HttpGet("GetLongUrl")]
        public async Task<IActionResult> GetLongUrl([FromBody] Urls url)
        {
            var shortUrl = await _urlShortener.GetLongUrl(url.ShortUrl);
            return Ok(shortUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
