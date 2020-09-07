using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RCC.Infrastructure.Data
{
    public abstract class AbstractRepository
    {

        public IDbConnection Connection { get; protected set; }

        public AbstractRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("RCC:ConnectionString").Value;

            var builder = new SqliteConnectionStringBuilder(connectionString);
            Connection = new SqliteConnection(builder.ConnectionString);
        }
    }
}
