I Used MSSQL,
created a db, and a simple table.

Using this:
CREATE TABLE Urls (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LongUrl NVARCHAR(MAX) NOT NULL,
    ShortUrl NVARCHAR(100) NOT NULL UNIQUE
)
