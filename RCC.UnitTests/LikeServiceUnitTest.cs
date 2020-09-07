using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RCC.Core.Domain;
using RCC.Core.Exceptions;
using RCC.Core.Repositories;
using RCC.Core.Services;
using RCC.Core.Services.Imp;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;

namespace RCC.UnitTests
{
    [TestClass]
    public class LikeServiceUnitTest
    {
        private readonly ILikeService _likeService;
        private readonly Mock<IArticlesLikeService> _articleService;
        private readonly Mock<ILikeRepository> _likeRepository;

        public LikeServiceUnitTest()
        {
            _articleService = new Mock<IArticlesLikeService>();
            _likeRepository = new Mock<ILikeRepository>();

            _likeService = new LikeService(_likeRepository.Object, _articleService.Object);
        }

        [TestMethod]
        public void LikeService_AddingALike()
        {
            //Arrange
            _articleService.Setup(a => a.Exists(It.IsAny<int>())).Returns(true);

            //Act
            _likeService.Add(1, true);

            //Assert
            _likeRepository.Verify((mocks => mocks.Add(It.IsAny<int>(), true)), Times.Once());
        }

        [TestMethod]
        public void LikeService_RemovingALike()
        {
            //Arrange
            _articleService.Setup(a => a.Exists(It.IsAny<int>())).Returns(true);

            //Act
            _likeService.Add(1, false);

            //Assert
            _likeRepository.Verify((mocks => mocks.Add(It.IsAny<int>(), false)), Times.Once());
        }

        [TestMethod]
        public void LikeService_AddingALikeToAnInvalidArticle()
        {
            //Arrange
            _articleService.Setup(a => a.Exists(It.IsAny<int>())).Returns(false);

            //Act

            //Assert
            Assert.ThrowsException<ArticleNotFoundException>(() => _likeService.Add(1, true));
        }
    }
}
