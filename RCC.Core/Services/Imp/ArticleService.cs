using RCC.Core.Domain;
using RCC.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Services.Imp
{
    public class ArticleService :  IArticlesLikeService
    {
        private readonly IArticleLikeRepository _articleRepository;

        public ArticleService(IArticleLikeRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public bool Exists(int articleId)
        {
            return _articleRepository.Exists(articleId);
        }

        public ArticlesLike Get(int articleID)
        {
            return _articleRepository.Get(articleID);
        }
    }
}
