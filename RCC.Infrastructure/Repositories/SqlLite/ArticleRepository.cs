using RCC.Core.Domain;
using RCC.Core.Repositories;
using RCC.Infrastructure.Data;
using System;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace RCC.Infrastructure.Repositories.SqlLite
{
    public class ArticleRepository : AbstractRepository, IArticleLikeRepository
    {
        public ArticleRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool Exists(int articleId)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM ArticlesLike WHERE Id = @articleId";

                Connection.Open();

                return Connection.ExecuteScalar<bool>(query, new { articleId });
            }
            catch (Exception)
            {
                //TODO: Add to a Log
            }
            finally
            {
                Connection.Close();
            }

            return false;
        }

        public ArticlesLike Get(int articleId)
        {
            ArticlesLike article = null;

            try
            {
                string query = "SELECT * FROM ArticlesLike WHERE Id = @id";

                Connection.Open();

                article = Connection.QuerySingle<ArticlesLike>(query, new { id = articleId });
            }
            catch (Exception)
            {
                //TODO: Add to a Log
            }
            finally
            {
                Connection.Close();
            }

            return article;
        }

        public void UpdateArticlesLike(int articleId, uint likes)
        {
            try
            {
                string query = "UPDATE ArticlesLike SET Likes = @Likes WHERE Id = @Id";

                Connection.Open();
                Connection.Query(query, new { Id = articleId, Likes = likes });
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
    }
}
