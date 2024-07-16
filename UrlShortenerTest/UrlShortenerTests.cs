using Moq;
using URLShortener;
using URLShortener.Services;
using URLShortener.Repositories;
namespace UrlShortenerTest
{
    public class UrlShortenerTests
    {
        private readonly Mock<UrlRepository> _mockrepo;
        private readonly UrlShortener _urlShortener;

        public UrlShortenerTests(Mock<UrlRepository> mockrepo, UrlShortener urlShortener)
        {
            _mockrepo = new Mock<UrlRepository>();
            _urlShortener = new UrlShortener(_mockrepo.Object);
        }

        [Fact]
        public async Task GetLongUrlNullTest()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _urlShortener.GetLongUrl(null));
            Assert.Equal("Short URL cannot be null", exception.Message);
        }
        [Fact]
        public async Task ShortenUrlAsyncLongURLNullTest()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _urlShortener.ShortenUrlAsync(null));
            Assert.Equal("Long URL cannot be null", exception.Message);
        }
        [Fact]
        public async Task GetLongUrlLongURlReturnsNull()
        {
            string shortUrl = "abc";

            _mockrepo.Setup(repo => repo.GetLongUrlAsync(shortUrl)).ReturnsAsync((string)null);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _urlShortener.GetLongUrl(shortUrl));
            Assert.Equal("Short URL cannot be found", exception.Message);
        }
    }
}