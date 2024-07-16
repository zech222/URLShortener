using URLShortener.Models;
using URLShortener.Repositories;
using static URLShortener.Controllers.UrlController;

namespace URLShortener.Services
{
    public class UrlShortener
    {
        private readonly UrlRepository _urlRepository;

        public UrlShortener(UrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<string> ShortenUrlAsync(Urls url)
        {
            //check url
            var shortUrl = GetShortUrl(url.LongUrl);
            await _urlRepository.AddUrlAsync(url.LongUrl, shortUrl);
            return shortUrl;
        }

        public async Task<string> GetLongUrl(string shortUrl)
        {
            //check url
            return await _urlRepository.GetLongUrlAsync(shortUrl);
            //check respond
        }
        private string GetShortUrl(string longUrl)
        {
            //TBD
            return "shortUrl";
        }
    }
}
