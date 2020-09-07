using Microsoft.AspNetCore.Mvc;
using RCC.Core.Domain;
using RCC.Core.Exceptions;
using RCC.Core.Services;
using RCC.WebApi.ViewModels;
using System;

namespace RCC.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikeController : ControllerBase
    {
        public readonly ILikeService _likeService;
        public readonly IArticlesLikeService _articleService;

        public LikeController(ILikeService likeService, IArticlesLikeService articleService)
        {
            _likeService = likeService;
            _articleService = articleService;
        }

        [HttpGet("{articleId}")]
        public ActionResult<LikeResponseViewModel> Get(int articleId)
        {
            var article = _articleService.Get(articleId);

            if (article == null)
                return NotFound();

            return Ok(new LikeResponseViewModel()
            {
                ArticleId = article.Id,
                Likes = article.Likes
            });
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult Post(LikeViewModel model)
        {
            try
            {
                _likeService.Add(model.ArticleId, model.Liked);
                return Ok();
            }
            catch (ArticleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception)
            {
                return UnprocessableEntity();
            }
        }
    }
}
