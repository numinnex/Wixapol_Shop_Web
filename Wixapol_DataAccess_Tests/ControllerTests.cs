using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.Controllers;
using WixapolShop.Areas.Customer.Models;
using Xunit;

namespace Wixapol_DataAccess_Tests
{
    public class ControllerTests
    {
        private readonly SearchController _searchController;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        public ControllerTests()
        {
            _searchController = new SearchController(_unitOfWork.Object);
        }

        [Fact]
        public void ProductsCart_QueryCheckNoCategory_WithMatchingName()
        {
            //Arrange
            SearchModel searchModel = new SearchModel { Categoryid = -1, Phrase = "Test" };

            List<Product> products = new();

            products.Add(new Product
            {
                Name = "Test Update",
            });

            _unitOfWork.Setup(x => x.Product.GetAllByNamePattern(searchModel.Phrase)).Returns(products);

            //Act
            var query = _searchController.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as List<Product>;

            //Assert
            Assert.Equal(products, result);
        }
        [Fact]
        public void ProductsCart_QueryCheckNoCategory_WithNoMatchingName()
        {
            //Arrange
            SearchModel searchModel = new SearchModel { Categoryid = -1, Phrase = "Test" };

            List<Product> products = new();

            products.Add(new Product
            {
                Name = "xD",
            });

            _unitOfWork.Setup(x => x.Product.GetAllByNamePattern(searchModel.Phrase)).Returns(new List<Product>());

            //Act
            var query = _searchController.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as List<Product>;

            //Assert
            Assert.Equal(new List<Product>(), result);
        }
        [Fact]
        public void ProductsCart_QueryCheckWithCategory_WithMatchingName()
        {
            //Arrange
            SearchModel searchModel = new SearchModel { Categoryid = 7, Phrase = "Test" };

            List<Product> products = new();

            products.Add(new Product
            {
                Name = "Test",
                CategoryId = searchModel.Categoryid
            });

            _unitOfWork.Setup(x => x.Product.GetAllByCategory(searchModel.Categoryid)).Returns(products);

            //Act
            var query = _searchController.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as List<Product>;

            //Assert
            Assert.Equal(products, result);
        }
        [Fact]
        public void ProductsCart_QueryCheckWithCategory_WithNoMatchingName()
        {
            //Arrange
            SearchModel searchModel = new SearchModel { Categoryid = 7, Phrase = "Test" };

            List<Product> products = new();

            products.Add(new Product
            {
                Name = "xD",
                CategoryId = searchModel.Categoryid
            });

            _unitOfWork.Setup(x => x.Product.GetAllByCategory(searchModel.Categoryid)).Returns(new List<Product>());

            //Act
            var query = _searchController.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as List<Product>;

            //Assert
            Assert.Equal(new List<Product>(), result);
        }
    }
}

