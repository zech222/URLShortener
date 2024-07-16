﻿using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static URLShortener.Controllers.UrlController;

namespace URLShortener.Repositories
{
    public class UrlRepository
    {
        private readonly string _connectionString;
        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddUrlAsync(string longURL,string shortURL)
        {
            using(IDbConnection db= new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Urls (LongUrl, ShortUrl) VALUES (@LongUrl,@ShortUrl )";
                await db.ExecuteAsync(query,new {LongUrl= longURL, ShortURL = shortURL});
            }
        }

        public async Task<string> GetLongUrlAsync(string shortUrl)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "Select longUrl from Urls where shortUrl=@ShortUrl";
                return await db.QueryFirstOrDefaultAsync<string>(query, new { ShortUrl = shortUrl });
            }
        }
    }
}
