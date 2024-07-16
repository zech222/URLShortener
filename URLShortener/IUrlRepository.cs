namespace URLShortener
{
    public interface IUrlRepository
    {
        Task AddUrlAsync(string shortUrl, string longUrl);
        Task<string> GetLongUrlAsync(string shortUrl);

    }
}
