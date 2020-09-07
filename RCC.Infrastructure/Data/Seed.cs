using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;
using Dapper;


namespace RCC.Infrastructure.Data
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void InitializeDatabase(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("RCC:ConnectionString").Value;

            var builder = new SqliteConnectionStringBuilder(connectionString);
            _dbConnection = new SqliteConnection(builder.ConnectionString);

            var dbFilePath = configuration.GetSection("RCC:DbFilePath").Value;

            if (!File.Exists(dbFilePath))
            {
                try
                {
                    _dbConnection.Open();

                    _dbConnection.Execute(@"CREATE TABLE IF NOT EXISTS [Like] (
                                                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                [articleid] INTEGER NOT NULL,
                                                [liked] BOOLEAN NOT NULL
                                            )");

                    _dbConnection.Execute(@"CREATE TABLE IF NOT EXISTS [ArticlesLike] (
                                                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                [Likes] INTEGER NOT NULL
                                            )");

                    string sQuery = "INSERT INTO ArticlesLike (Likes) VALUES(@likes)";
       
                    _dbConnection.Execute(sQuery, new { likes = 0 });
                    _dbConnection.Execute(sQuery, new { likes = 0 });
                    _dbConnection.Execute(sQuery, new { likes = 0 });

                    sQuery = "INSERT INTO Like (articleID,liked) VALUES(@articleId, @liked)";

                    _dbConnection.Execute(sQuery, new { articleId = 1, liked = true });
                    _dbConnection.Execute(sQuery, new { articleId = 1, liked = true });
                    _dbConnection.Execute(sQuery, new { articleId = 1, liked = false });
                    _dbConnection.Execute(sQuery, new { articleId = 2, liked = true });
                    _dbConnection.Execute(sQuery, new { articleId = 2, liked = false });
                    _dbConnection.Execute(sQuery, new { articleId = 3, liked = false });
                    _dbConnection.Execute(sQuery, new { articleId = 3, liked = false });

                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }
    }
}
