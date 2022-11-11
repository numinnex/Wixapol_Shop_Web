using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.Controllers;
using WixapolShop.Areas.Customer.ViewModels;
using Xunit;

namespace Wixapol_DataAccess_Tests
{
    public class UserOrdersControllerTests
    {
        private readonly UserOrdersController _sut;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<ClaimsPrincipal> User = new Mock<ClaimsPrincipal>();
        public UserOrdersControllerTests()
        {
            _sut = new UserOrdersController(_unitOfWork.Object);
        }


        [Fact]
        public void OrderDetailsTests()
        {
            //Arrange

            int saleId = 1;
            SaleDisplayVM vm = new();

            var sale = new Sale()
            {
                Id = saleId,
                OrderId = 1,
                SaleDate = DateTime.Now,
            };
            List<SaleDetail> saleDetail = new();

            var saleDt = new SaleDetail()
            {
                SaleId = saleId,
                ProductId = 1,
                SubTotal = 123.23
            };
            saleDetail.Add(saleDt);

            Order order = new Order()
            {
                PostalCode = "33-320",
                Name = "Test",
            };
            List<Product> products = new();

            Product product = new()
            {
                Id = 1,
                Name = "Test",
                QuantityInStock = 10
            };

            products.Add(product);

            vm.Sale = sale;
            vm.Products = products;
            vm.Sale.Order = order;
            vm.Sale.SaleDetail = saleDetail;

            vm.SaleId = saleId;
            _unitOfWork.Setup(x => x.Sale.GetById(saleId)).Returns(sale);
            _unitOfWork.Setup(x => x.SaleDetail.GetBySaleId(saleId)).Returns(saleDetail);
            _unitOfWork.Setup(x => x.Order.GetById(vm.Sale.OrderId)).Returns(order);
            _unitOfWork.Setup(x => x.Product.GetById(vm.Sale.SaleDetail[0].ProductId)).Returns(product);


            //Act
            var query = _sut.OrderDetails(saleId) as ViewResult;

            var result = query.ViewData.Model as SaleDisplayVM;


            //Assert
            Assert.Equal(result.Sale, vm.Sale);
            Assert.Equal(result.Sale.Order, vm.Sale.Order);
            Assert.Equal(result.Sale.SaleDetail, vm.Sale.SaleDetail);
            Assert.Equal(result.Products, vm.Products);

        }

        [Theory]
        [InlineData("approved", 0)]
        [InlineData("shipped", 1)]
        [InlineData("processing", 2)]
        public void OrderTests(string expected, int idx)
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();

            //TODO - this is how to fake user

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }, "TestAuthentication"
                ));

            List<Sale> sale = new();
            Sale saleD = new Sale()
            {
                UserId = userId,
                Id = 1,
                SaleDate = DateTime.Now,
                Total = 123.23,
                OrderStatus = "approved",
            };
            Sale saleC = new Sale()
            {
                UserId = userId,
                Id = 1,
                SaleDate = DateTime.Now,
                Total = 123.23,
                OrderStatus = "shipped"
            };
            Sale saleX = new Sale()
            {
                UserId = userId,
                Id = 1,
                SaleDate = DateTime.Now,
                Total = 123.23,
                OrderStatus = "processing"
            };
            sale.Add(saleD);
            sale.Add(saleC);
            sale.Add(saleX);

            _unitOfWork.Setup(x => x.Sale.GetByUserId(userId)).Returns(sale);
            //Act
            _sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var query = _sut.Orders(expected) as ViewResult;

            var result = query.Model as List<Sale>;

            //Assert

            Assert.Equal(sale[idx].OrderStatus, result[0].OrderStatus);
        }

    }

}
