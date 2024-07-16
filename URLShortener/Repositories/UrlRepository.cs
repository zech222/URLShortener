using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using URLShortener.Models;
using static URLShortener.Controllers.UrlController;

namespace URLShortener.Repositories
{
    public class UrlRepository: IUrlRepository
    {
        private readonly string _connectionString;
        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddUrlAsync(string shortUrl, string longUrl)
        {
            using(IDbConnection db= new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Urls (LongUrl, ShortUrl) VALUES (@LongUrl,@ShortUrl )";
                await db.ExecuteAsync(query,new {LongUrl= longUrl, ShortUrl = shortUrl});
            }
        }

        public async Task<string> GetLongUrlAsync(string shortUrl)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "Select longUrl from Urls where shortUrl=@ShortUrl";
                return await db.QueryFirstOrDefaultAsync<string>(query, new { ShortUrl = shortUrl});
            }
        }

        public async Task<Urls> GetLastIndex()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = @"SELECT ISNULL(Id,0) as Id ,ISNULL(ShortUrl,'') as ShortUrl  FROM Urls WHERE Id = (SELECT MAX(ISNULL(Id,0)) FROM Urls);";
                var result=  await db.QueryFirstOrDefaultAsync<Urls>(query);
                if (result == null)
                {
                    return new Urls { Id = 0, ShortUrl = "" };
                }
                return new Urls { Id=result.Id,ShortUrl=result.ShortUrl};
            }
        }

    }
}
