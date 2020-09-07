using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RCC.Core.Domain;
using RCC.Core.Repositories;
using RCC.Core.Services;
using RCC.Core.Services.Imp;

namespace RCC.UnitTests
{
    [TestClass]
    public class ArticleServiceUnitTest
    {
        private readonly IArticlesLikeService _articleService;
        private readonly Mock<IArticleLikeRepository> _articleRepository;

        public ArticleServiceUnitTest()
        {
            _articleRepository = new Mock<IArticleLikeRepository>();

            _articleService = new ArticleService(_articleRepository.Object);
        }

        [TestMethod]
        public void ArticleService_GetAnExistingArticle()
        {
            //Arrange
            _articleRepository.Setup(a => a.Get(It.IsAny<int>()))
                              .Returns(new ArticlesLike() { Id = 1 });

            //Act
            var result = _articleService.Get(1);

            //Assert
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void ArticleService_GetAnUnexistentArticle()
        {
            //Arrange
            _articleRepository.Setup(a => a.Get(It.IsAny<int>()))
                              .Returns(() => null);

            //Act
            var result = _articleService.Get(1);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ArticleService_VeryfyingExistingArticle()
        {
            //Arrange
            _articleRepository.Setup(a => a.Exists(It.IsAny<int>()))
                              .Returns(true);

            //Act
            var result = _articleService.Exists(1);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArticleService_VeryfyingUnexistentArticle()
        {
            //Arrange
            _articleRepository.Setup(a => a.Exists(It.IsAny<int>()))
                              .Returns(false);

            //Act
            var result = _articleService.Exists(1);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
