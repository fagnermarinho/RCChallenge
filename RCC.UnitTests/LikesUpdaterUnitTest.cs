using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RCC.Core.Domain;
using RCC.Core.Repositories;
using RCC.Core.Services;
using RCC.Core.Services.Imp;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace RCC.UnitTests
{
    [TestClass]
    public class LikesUpdaterUnitTest
    {
        private ILikesUpdater _likesUpdater;
        private Mock<IConfiguration> _configuration;
        private readonly Mock<ILikeRepository> _likeRepository;
        private readonly Mock<IArticleLikeRepository> _articleRepository;

        IList<Like> _likes;
        IList<ArticlesLike> _articleLiked;

        public LikesUpdaterUnitTest()
        {
            _configuration = new Mock<IConfiguration>();
            _likeRepository = new Mock<ILikeRepository>();
            _articleRepository = new Mock<IArticleLikeRepository>();
        }

        [TestMethod]
        public void LikesUpdate_GetMaxItemsToUpdateFromConfiguration()
        {
            //Arrange

            InitializeMocks();

            _likeRepository.Setup(l => l.GetLikes(It.IsAny<int>())).Returns(_likes);

            _articleRepository.Setup(a => a.UpdateArticlesLike(It.IsAny<int>(), It.IsAny<uint>()));

            var article = _articleLiked.First(a => a.Id == 1);
            _articleRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(article);

            _likesUpdater = new LikesUpdater(_configuration.Object, _likeRepository.Object, _articleRepository.Object);

            //Act
            _likesUpdater.Execute();

            //Assert
            _configuration.Verify((mocks => mocks.GetSection("RCC:MaxItemsToUpdate")), Times.Once());
        }

        [TestMethod]
        public void LikesUpdate_AddingTwoLikes()
        {
            //Arrange

            InitializeMocks();

            _likes.Add(new Like() { Id = 1, ArticleId = 1, Liked = true });
            _likes.Add(new Like() { Id = 2, ArticleId = 1, Liked = true });

            _likeRepository.Setup(l => l.GetLikes(It.IsAny<int>())).Returns(_likes);

            _articleRepository.Setup(a => a.UpdateArticlesLike(It.IsAny<int>(), It.IsAny<uint>()));

            var article = _articleLiked.First(a => a.Id == 1);
            _articleRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(article);

            _likesUpdater = new LikesUpdater(_configuration.Object, _likeRepository.Object, _articleRepository.Object);

            //Act
            _likesUpdater.Execute();

            //Assert
            _articleRepository.Verify(a => a.UpdateArticlesLike(It.Is<int>(args => args == 1), It.Is<uint>(args => args == 2)));
        }

        [TestMethod]
        public void LikesUpdate_AddingTwoLikesAndOneDislikes()
        {
            //Arrange

            InitializeMocks();

            _likes.Add(new Like() { Id = 1, ArticleId = 1, Liked = true });
            _likes.Add(new Like() { Id = 2, ArticleId = 1, Liked = false });
            _likes.Add(new Like() { Id = 3, ArticleId = 1, Liked = true });

            _likeRepository.Setup(l => l.GetLikes(It.IsAny<int>())).Returns(_likes);

            _articleRepository.Setup(a => a.UpdateArticlesLike(It.IsAny<int>(), It.IsAny<uint>()));

            var article = _articleLiked.First(a => a.Id == 1);
            _articleRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(article);

            _likesUpdater = new LikesUpdater(_configuration.Object, _likeRepository.Object, _articleRepository.Object);

            //Act
            _likesUpdater.Execute();

            //Assert
            _articleRepository.Verify(a => a.UpdateArticlesLike(It.Is<int>(args => args == 1), It.Is<uint>(args => args == 1)));
        }

        [TestMethod]
        public void LikesUpdate_AddingOnlyDislikes()
        {
            //Arrange

            InitializeMocks();

            _likes.Add(new Like() { Id = 1, ArticleId = 1, Liked = false });
            _likes.Add(new Like() { Id = 2, ArticleId = 1, Liked = false });
            _likes.Add(new Like() { Id = 3, ArticleId = 1, Liked = true });

            _likeRepository.Setup(l => l.GetLikes(It.IsAny<int>())).Returns(_likes);

            _articleRepository.Setup(a => a.UpdateArticlesLike(It.IsAny<int>(), It.IsAny<uint>()));

            var article = _articleLiked.First(a => a.Id == 1);
            _articleRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(article);

            _likesUpdater = new LikesUpdater(_configuration.Object, _likeRepository.Object, _articleRepository.Object);

            //Act
            _likesUpdater.Execute();

            //Assert
            _articleRepository.Verify(a => a.UpdateArticlesLike(It.Is<int>(args => args == 1), It.Is<uint>(args => args == 0)));
        }

        [TestMethod]
        public void LikesUpdate_DeletingLikesAfterUpdateArticles()
        {
            //Arrange

            InitializeMocks();

            _likes.Add(new Like() { Id = 1, ArticleId = 1, Liked = true });
            _likes.Add(new Like() { Id = 2, ArticleId = 1, Liked = false });
            _likes.Add(new Like() { Id = 3, ArticleId = 1, Liked = true });

            _likeRepository.Setup(l => l.GetLikes(It.IsAny<int>())).Returns(_likes);

            _articleRepository.Setup(a => a.UpdateArticlesLike(It.IsAny<int>(), It.IsAny<uint>()));

            var article = _articleLiked.First(a => a.Id == 1);
            _articleRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(article);

            _likesUpdater = new LikesUpdater(_configuration.Object, _likeRepository.Object, _articleRepository.Object);

            //Act
            _likesUpdater.Execute();

            //Assert
            _likeRepository.Verify(a => a.Delete(It.Is<ICollection<Like>>(args => args.Count == 3)));
        }

        private void InitializeMocks()
        {
            //Mock _configuration
            _configuration = new Mock<IConfiguration>();
            var _configurationSection = new Mock<IConfigurationSection>();
            _configurationSection.Setup(a => a.Value).Returns("10");
            _configuration.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);
            _configuration.Setup(a => a.GetSection("RCC:MaxItemsToUpdate")).Returns(_configurationSection.Object);


            //Fake Like list
            _likes = new List<Like>();

            //Fake Article's like list
            _articleLiked = new List<ArticlesLike> {new ArticlesLike() { Id = 1},
                                                    new ArticlesLike() { Id = 2},
                                                    new ArticlesLike() { Id = 3}};
        }
    }
}
