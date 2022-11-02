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
using WixapolShop.Areas.Customer.ViewModels;
using Xunit;

namespace Wixapol_DataAccess_Tests
{
    public class ProductSearchControllerTests
    {
        private readonly SearchProductController _sut;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        public ProductSearchControllerTests()
        {
            _sut = new SearchProductController(_unitOfWork.Object);
        }

        [Fact]
        public void ProductsCart_QueryCheckNoCategory_WithMatchingName()
        {
            //Arrange
            ProductSearchVM productVM = new();

            SearchModel searchModel = new SearchModel { Categoryid = -1, Phrase = "Test" };
            productVM.query = searchModel;

            List<Product> products = new();
            BaseSpecification baseSpec = new();
            AdvancedSpecification advancedSpec = new();
            PhysicalSpecification physSpec = new();

            baseSpec.SpecValue = "Test";
            baseSpec.SpecName = "Test";
            advancedSpec.SpecValue = "Test";
            advancedSpec.SpecName = "Test";
            physSpec.SpecValue = "Test";
            physSpec.SpecName = "Test";

            products.Add(new Product
            {
                Name = "Test Update",
                BaseSpecId = 1,
                AdvancedSpecId = 1,
                PhysicalSpecId = 1,
                Category = new()
                {
                    Id = 1,
                    Name = "Test Category"
                },
                Producent = new()
                {
                    Id = 1,
                    Name = "Test producent",
                },
                RetailPrice = 100,

            });

            productVM.Products = products;
            productVM.MaxPrice = 100;

            _unitOfWork.Setup(x => x.Product.GetAllByNamePattern(searchModel.Phrase)).Returns(products);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].BaseSpecId, Specification.Basic)).Returns(baseSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].AdvancedSpecId, Specification.Advanced)).Returns(advancedSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].PhysicalSpecId, Specification.Physical)).Returns(physSpec);

            productVM.ProductsByCategory = productVM.Products.GroupBy(x => x.Category.Id, p => p.Category.Name).ToDictionary(x => x.Key, p => p.ToList());
            productVM.ProductsByProducent = productVM.Products.GroupBy(x => x.Producent.Name, p => p.Name).ToDictionary(x => x.Key, p => p.ToList());
            productVM.ProductsByPrice = new SortedDictionary<double, List<string>>(productVM.Products.GroupBy(x => x.RetailPrice, p => p.Name).ToDictionary(x => x.Key, p => p.ToList()));

            //Act
            var query = _sut.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as ProductSearchVM;

            //Assert
            Assert.Equal(productVM.Products, result.Products);
            Assert.Equal(productVM.ProductsByCategory, result.ProductsByCategory);
            Assert.Equal(productVM.ProductsByProducent, result.ProductsByProducent);
            Assert.Equal(productVM.ProductsByPrice, result.ProductsByPrice);
            Assert.Equal(productVM.Products[0].BaseSpec, result.Products[0].BaseSpec);
            Assert.Equal(productVM.Products[0].AdvancedSpec, result.Products[0].AdvancedSpec);
            Assert.Equal(productVM.Products[0].PhysicalSpec, result.Products[0].PhysicalSpec);
        }
        [Fact]
        public void ProductsCart_QueryCheckNoCategory_WithNoMatchingName()
        {
            //Arrange
            ProductSearchVM productVM = new();

            SearchModel searchModel = new SearchModel { Categoryid = -1, Phrase = "Test" };
            productVM.query = searchModel;

            List<Product> products = new();
            BaseSpecification baseSpec = new();
            AdvancedSpecification advancedSpec = new();
            PhysicalSpecification physSpec = new();

            baseSpec.SpecValue = "Test";
            baseSpec.SpecName = "Test";
            advancedSpec.SpecValue = "Test";
            advancedSpec.SpecName = "Test";
            physSpec.SpecValue = "Test";
            physSpec.SpecName = "Test";

            products.Add(new Product
            {
                Name = "XDD",
                BaseSpecId = 1,
                AdvancedSpecId = 1,
                PhysicalSpecId = 1,
                Category = new()
                {
                    Id = 1,
                    Name = "Test Category"
                },
                Producent = new()
                {
                    Id = 1,
                    Name = "Test producent",
                },
                RetailPrice = 100,

            });

            productVM.Products = products;
            productVM.MaxPrice = 100;

            _unitOfWork.Setup(x => x.Product.GetAllByNamePattern(searchModel.Phrase)).Returns(new List<Product>());
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].BaseSpecId, Specification.Basic)).Returns(baseSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].AdvancedSpecId, Specification.Advanced)).Returns(advancedSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].PhysicalSpecId, Specification.Physical)).Returns(physSpec);

            //Act
            var query = _sut.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as ProductSearchVM;

            //Assert
            Assert.Equal(new List<Product>(), result.Products);
        }
        [Fact]
        public void ProductsCart_QueryCheckWithCategory_WithMatchingName()
        {
            //Arrange
            ProductSearchVM productVM = new();

            SearchModel searchModel = new SearchModel { Categoryid = 1, Phrase = "Test" };
            productVM.query = searchModel;

            List<Product> products = new();
            BaseSpecification baseSpec = new();
            AdvancedSpecification advancedSpec = new();
            PhysicalSpecification physSpec = new();

            baseSpec.SpecValue = "Test";
            baseSpec.SpecName = "Test";
            advancedSpec.SpecValue = "Test";
            advancedSpec.SpecName = "Test";
            physSpec.SpecValue = "Test";
            physSpec.SpecName = "Test";

            products.Add(new Product
            {
                Name = "Test Update",
                BaseSpecId = 1,
                AdvancedSpecId = 1,
                PhysicalSpecId = 1,
                Category = new()
                {
                    Id = 1,
                    Name = "Test Category"
                },
                Producent = new()
                {
                    Id = 1,
                    Name = "Test producent",
                },
                RetailPrice = 100,

            });

            productVM.Products = products;
            productVM.MaxPrice = 100;

            _unitOfWork.Setup(x => x.Product.GetAllByCategoryAndName(searchModel.Categoryid, searchModel.Phrase)).Returns(products);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].BaseSpecId, Specification.Basic)).Returns(baseSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].AdvancedSpecId, Specification.Advanced)).Returns(advancedSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].PhysicalSpecId, Specification.Physical)).Returns(physSpec);

            productVM.ProductsByCategory = productVM.Products.GroupBy(x => x.Category.Id, p => p.Category.Name).ToDictionary(x => x.Key, p => p.ToList());
            productVM.ProductsByProducent = productVM.Products.GroupBy(x => x.Producent.Name, p => p.Name).ToDictionary(x => x.Key, p => p.ToList());
            productVM.ProductsByPrice = new SortedDictionary<double, List<string>>(productVM.Products.GroupBy(x => x.RetailPrice, p => p.Name).ToDictionary(x => x.Key, p => p.ToList()));
            //Act
            var query = _sut.ProductsCart(searchModel) as ViewResult;

            var result = query.ViewData.Model as ProductSearchVM;

            //Assert
            Assert.Equal(productVM.Products, result.Products);
            Assert.Equal(productVM.ProductsByCategory, result.ProductsByCategory);
            Assert.Equal(productVM.ProductsByProducent, result.ProductsByProducent);
            Assert.Equal(productVM.ProductsByPrice, result.ProductsByPrice);
            Assert.Equal(productVM.Products[0].BaseSpec, result.Products[0].BaseSpec);
            Assert.Equal(productVM.Products[0].AdvancedSpec, result.Products[0].AdvancedSpec);
            Assert.Equal(productVM.Products[0].PhysicalSpec, result.Products[0].PhysicalSpec);
        }

        [Fact]
        public void FilterProductsCart_QueryWithCategory()
        {
            //Arrange
            ProductSearchVM productVM = new();

            SearchModel searchModel = new SearchModel { Categoryid = 1, Phrase = "Test" };
            productVM.query = searchModel;

            List<Product> products = new();
            BaseSpecification baseSpec = new();
            AdvancedSpecification advancedSpec = new();
            PhysicalSpecification physSpec = new();

            baseSpec.SpecValue = "Test";
            baseSpec.SpecName = "Test";
            advancedSpec.SpecValue = "Test";
            advancedSpec.SpecName = "Test";
            physSpec.SpecValue = "Test";
            physSpec.SpecName = "Test";

            products.Add(new Product
            {
                Name = "Test Update",
                BaseSpecId = 1,
                AdvancedSpecId = 1,
                PhysicalSpecId = 1,
                Category = new()
                {
                    Id = 1,
                    Name = "Test Category"
                },
                Producent = new()
                {
                    Id = 1,
                    Name = "Test producent",
                },
                RetailPrice = 100,

            });

            productVM.Products = products;
            productVM.MaxPrice = 100;

            _unitOfWork.Setup(x => x.Product.GetAllByNamePattern(searchModel.Phrase)).Returns(products);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].BaseSpecId, Specification.Basic)).Returns(baseSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].AdvancedSpecId, Specification.Advanced)).Returns(advancedSpec);
            _unitOfWork.Setup(x => x.Specification.GetById(products[0].PhysicalSpecId, Specification.Physical)).Returns(physSpec);

            productVM.ProductsByCategory = productVM.Products.GroupBy(x => x.Category.Id, p => p.Category.Name).ToDictionary(x => x.Key, p => p.ToList());
            productVM.ProductsByProducent = productVM.Products.GroupBy(x => x.Producent.Name, p => p.Name).ToDictionary(x => x.Key, p => p.ToList());
            productVM.ProductsByPrice = new SortedDictionary<double, List<string>>(productVM.Products.GroupBy(x => x.RetailPrice, p => p.Name).ToDictionary(x => x.Key, p => p.ToList()));

            productVM.CategoryIdList = new List<int>() { 1 };
            //Act
            var query = _sut.FilterProductsCart(productVM) as ViewResult;

            var result = query.ViewData.Model as ProductSearchVM;

            //Assert
            Assert.Equal(productVM.Products, result.Products);
            Assert.Equal(productVM.ProductsByCategory, result.ProductsByCategory);
            Assert.Equal(productVM.ProductsByProducent, result.ProductsByProducent);
            Assert.Equal(productVM.ProductsByPrice, result.ProductsByPrice);
            Assert.Equal(productVM.Products[0].BaseSpec, result.Products[0].BaseSpec);
            Assert.Equal(productVM.Products[0].AdvancedSpec, result.Products[0].AdvancedSpec);
            Assert.Equal(productVM.Products[0].PhysicalSpec, result.Products[0].PhysicalSpec);
        }

    }
}

