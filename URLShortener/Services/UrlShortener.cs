using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<string> ShortenUrlAsync(string longUrl)
        {
            if (longUrl.IsNullOrEmpty())
                throw new ArgumentException("Long URL cannot be null");

            var shortUrl = await CreateShortUrl();
            await _urlRepository.AddUrlAsync(shortUrl,longUrl);
            return shortUrl;
        }

        public async Task<string> GetLongUrl(string shortUrl)
        {
            if (shortUrl.IsNullOrEmpty())
                throw new ArgumentException("Short URL cannot be null");

            var longUrl = await _urlRepository.GetLongUrlAsync(shortUrl);

            if (longUrl.IsNullOrEmpty())
                throw new ArgumentException("Short URL cannot be found");

            return longUrl;
        }
        private async Task<string> CreateShortUrl()
        {
            var currUrl = await _urlRepository.GetLastIndex();
            var url = ToBase26(currUrl.Id);
            return url;
        }

        private string ToBase26(int number)
        {
            var abc = "abcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder();
            while (number >= 0)
            {
                result.Insert(0, abc[number % 26]);
                number = number / 26-1;
            }
            return result.ToString();
        }
    }
}
