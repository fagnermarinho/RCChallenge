using RCC.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Repositories
{
    public interface IArticleLikeRepository
    {
        ArticlesLike Get(int id);
        void UpdateArticlesLike(int articleId, uint likes);
        bool Exists(int articleId);
    }
}
