using Microsoft.Extensions.Configuration;
using RCC.Core.Repositories;
using RCC.Infrastructure.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;
using System;
using RCC.Core.Domain;
using System.Collections.Generic;

namespace RCC.Infrastructure.Repositories.SqlLite
{
    public class LikeRepository : AbstractRepository, ILikeRepository
    {
        public LikeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void Add(int articleId, bool liked)
        {
            try { 
                string query = "INSERT INTO Like (articleid, liked)"
                                + " VALUES(@id, @liked)";
                Connection.Open();
                Connection.Execute(query, new { id = articleId, liked = liked });
            }
            finally
            {
                Connection.Close();
            }
        }

        public void Delete(ICollection<Like> items)
        {
            try
            {
                string query = "DELETE FROM Like WHERE Id = @Id";
                Connection.Open();

                items.ToList().ForEach(l => { Connection.Execute(query, new { Id = l.Id }); });

            }
            catch (Exception)
            {
                //TODO: Add to a Log
            }
            finally
            {
                Connection.Close();
            }
        }

        public ICollection<Like> GetLikes(int maxItems)
        {
            IEnumerable<Like> likes = null;

            try
            {
                string query = "SELECT * FROM Like LIMIT {=maxItems}";

                Connection.Open();

                likes = Connection.Query<Like>(query, new { maxItems = maxItems });
            }
            catch (Exception)
            {
                //TODO: Add to a Log
            }
            finally
            {
                Connection.Close();
            }

            return likes.ToList();
        }
    }
}
