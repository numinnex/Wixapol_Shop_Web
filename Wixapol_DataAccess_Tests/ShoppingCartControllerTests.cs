using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
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
    public class ShoppingCartControllerTests
    {
        private readonly ShoppingCartController _sut;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<ClaimsPrincipal> User = new Mock<ClaimsPrincipal>();
        public ShoppingCartControllerTests()
        {
            _sut = new ShoppingCartController(_unitOfWork.Object);
        }

    }
}
