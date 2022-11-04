using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Implementation;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.DTO;
using Wixapol_DataAccess.UnitOfWork.Implementation;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Xunit;

namespace Wixapol_DataAccess_Tests
{
    public sealed class DataAccessTests
    {
        private readonly IUnitOfWork _sut;



        public DataAccessTests()
        {

            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DefualtConnection")]).Returns("Server=(localdb)\\MSSQLLocalDB;Database=WixapolDB;Trusted_Connection=true");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Product, ProductDTO>(It.IsAny<Product>()));
            mockMapper.Setup(x => x.Map<Product, ProductDTOWithId>(It.IsAny<Product>()));

            _sut = new UnitOfWork(mockConfiguration.Object, mockMapper.Object);

        }

        [Theory]
        [InlineData("Mouse", 1)]
        [InlineData("Keyboard", 7)]
        [InlineData("Monitor", 9)]
        [InlineData("Printer", 1002)]
        [InlineData("Phone", 1003)]
        public void LoadCategoryById(string expected, int id)
        {
            var categoryName = _sut.Category.GetById(id).Name;

            Assert.Equal(expected, categoryName);
        }
        [Fact]
        public void LoadAllCategories()
        {
            var categoryList = _sut.Category.GetAll();
            Assert.True(categoryList != null);
        }
        [Theory]
        [InlineData("Razer", 2002)]
        [InlineData("SteelSeries", 2009)]
        public void LoadProducentById(string expected, int id)
        {
            var producentName = _sut.Producent.GetById(id).Name;

            Assert.Equal(expected, producentName);
        }
        [Fact]
        public void LoadAllProducents()
        {
            var producentList = _sut.Producent.GetAll();
            Assert.True(producentList != null);
        }

        [Theory]
        [InlineData("Huntsman Elite", 3002)]
        [InlineData("Huntsman V2", 3003)]
        [InlineData("Blackwidow V3", 3004)]
        public void LoadProductById(string expected, int id)
        {
            var productName = _sut.Product.GetById(id).Name;

            Assert.Equal(expected, productName);
        }

        [Fact]
        public void LoadAllProducts()
        {
            var productList = _sut.Product.GetAll();
            Assert.True(productList != null);
        }
        [Theory]
        [InlineData(new int[] { 7, 7, 7 })]
        public void LoadProductsWithCategory(int[] expected)
        {
            var productList = _sut.Product.GetAll().Take(3).ToList();

            for (int i = 0; i < productList.Count; ++i)
            {
                Assert.Equal(expected[i], productList[i].Category.Id);
            }
        }
        [Theory]
        [InlineData(new int[] { 2002, 2003, 2004 })]
        public void LoadProductsWithProducent(int[] expected)
        {
            var productList = _sut.Product.GetAll().Take(3).ToList();

            for (int i = 0; i < productList.Count; ++i)
            {
                Assert.Equal(expected[i], productList[i].Producent.Id);
            }
        }

        [Theory]
        [InlineData("Test;Test2", 1)]
        public void LoadBasicSpecificationById(string expected, int id)
        {
            var baseSpecName = _sut.Specification.GetById(id, Specification.Basic);

            Assert.Equal(expected, baseSpecName.SpecName);

        }

        [Theory]
        [InlineData("AdvTest;AdvTest2", 1)]
        public void LoadAdvancedSpecificationById(string expected, int id)
        {
            var advancedSpecName = _sut.Specification.GetById(id, Specification.Advanced);

            Assert.Equal(expected, advancedSpecName.SpecName);

        }
        [Theory]
        [InlineData("PhysTest;PhysTest2", 1)]
        public void LoadPhysicalSpecificationById(string expected, int id)
        {
            var physSpecName = _sut.Specification.GetById(id, Specification.Physical);

            Assert.Equal(expected, physSpecName.SpecName);

        }
        [Theory]
        [InlineData("6344ee1e-7a85-4fb7-874b-b47637600edb", 13)]
        public void LoadShoppingCartById(string id, int expected)
        {
            var shoppingCart = _sut.ShoppingCart.GetShoppingCartByUserId(id).FirstOrDefault();

            Assert.Equal(expected, shoppingCart.Count);
        }



        //[MemberData(nameof(TestData))]
        //Multiple inputs 
        //public static IEnumerable<object[]> TestData()
        //{
        //    yield return new object[] { };
        //}

    }

    //[ClassData(typeof(Data))]
    //Multiple Inputs with class
    //    public sealed class Data : IEnumerable<object[]>
    //    {
    //        public IEnumerator<object[]> GetEnumerator()
    //        {
    //            yield return new object[] { };
    //        }

    //        IEnumerator IEnumerable.GetEnumerator()
    //        {
    //            return GetEnumerator();
    //        }
    //    }


}
