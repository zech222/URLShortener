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

        public async Task<string> ShortenUrlAsync(Urls Url)
        {
            var shortUrl = GetShortUrl(Url.LongUrl);
            await _urlRepository.AddUrlAsync(Url.LongUrl, shortUrl);
            return shortUrl;
        }
        private string GetShortUrl(string longUrl)
        {
            //TBD
            return "shortUrl";
        }
    }
}
