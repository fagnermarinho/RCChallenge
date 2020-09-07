using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using RCC.Core.Repositories;

namespace RCC.Core.Services.Imp
{
    public class LikesUpdater : ILikesUpdater
    {
        private readonly IConfiguration _configuration;
        private readonly ILikeRepository _likeRepository;
        private readonly IArticleLikeRepository _articleRepository;
        private readonly int _maxItemsToProcess;

        public LikesUpdater(IConfiguration configuration, ILikeRepository likeRepository, IArticleLikeRepository articleRepository)
        {
            _configuration = configuration;
            _likeRepository = likeRepository;
            _articleRepository = articleRepository;

            _maxItemsToProcess = Convert.ToInt32(_configuration.GetSection("RCC:MaxItemsToUpdate").Value);
        }
        public void Execute()
        {
            var items = _likeRepository.GetLikes(_maxItemsToProcess);

            if (items.Count == 0)
                return;

            var groupLikes = items.GroupBy(g => new { g.ArticleId })
                                    .Select(group => new
                                    {
                                        articleId = group.Key,
                                        Liked = group.Where(x => x.Liked == true).Count(),
                                        Disliked = group.Where(x => x.Liked == false).Count()
                                    })
                                    .Select(c => new { c.articleId, totalLikes = c.Liked - c.Disliked });


            groupLikes.ToList().ForEach(u =>
            {
                var article = _articleRepository.Get(u.articleId.ArticleId);

                if (article != null)
                {
                    long total = article.Likes + u.totalLikes;
                    uint likes = total < 0 ? 0 : Convert.ToUInt32(total);

                    _articleRepository.UpdateArticlesLike(article.Id, likes);
                }
            });

            _likeRepository.Delete(items);
        }
    }
}
