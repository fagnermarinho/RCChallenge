using RCC.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Services
{
    public interface IArticlesLikeService
    {
        ArticlesLike Get(int articleID);
        bool Exists(int articleId);
    }
}
