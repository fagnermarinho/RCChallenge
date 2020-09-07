using RCC.Core.Exceptions;
using RCC.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCC.Core.Services.Imp
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IArticlesLikeService _articleService;

        public LikeService(ILikeRepository likeRepository, IArticlesLikeService articleService)
        {
            _likeRepository = likeRepository;
            _articleService = articleService;
        }

        public void Add(int articleId, bool liked)
        {
            if (_articleService.Exists(articleId) == false)
                throw new ArticleNotFoundException();

            _likeRepository.Add(articleId, liked);
        }
    }
}
