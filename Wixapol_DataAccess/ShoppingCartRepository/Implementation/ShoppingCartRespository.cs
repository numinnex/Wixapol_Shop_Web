using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.ShoppingCartRepository.Interfaces;

namespace Wixapol_DataAccess.ShoppingCartRepository.Implementation
{
    public class ShoppingCartRespository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly IConfiguration _config;
        public ShoppingCartRespository(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public void CreateShoppingCart(ShoppingCart shoppingCart)
        {
            SaveData<dynamic>("db_product.spShoppingCart_Insert", new
            {
                ProductId = shoppingCart.ProductId,
                UserId = shoppingCart.UserId,
                Count = shoppingCart.Count
            },
                "DefualtConnection");
        }


        public void DeleteShoppingCart(int? id)
        {
            SaveData<dynamic>("db_product.spShoppingCart_Delete", new { Id = id }, "DefualtConnection");
        }

        public List<ShoppingCart> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCart> GetShoppingCartByUserId(string id)
        {
            return LoadData<dynamic>("db_product.spShoppingCart_GetByUserId", new { UserId = id }, "DefualtConnection");
        }

        public void UpdateShoppingCartProductCount(int shoppingCartId, int count)
        {
            SaveData("db_product.spShoppingCart_UpdateCountByProductId", new { Id = shoppingCartId, Count = count }, "DefualtConnection");
        }

        public void UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            SaveData("db_product.spShoppingCart_UpdatePrices", new
            {
                Id = shoppingCart.Id,
                SubTotal = shoppingCart.SubTotal,
                TaxAmount = shoppingCart.TaxAmount,
                Total = shoppingCart.Total
            },
                "DefualtConnection");
        }

        public void DecrementShoppingCartProductCount(int shoppingCartId)
        {
            SaveData("db_product.spShoppingCart_DecrementCount", new { cartId = shoppingCartId }, "DefualtConnection");
        }

        public void IncrementShoppingCartProductCount(int shoppingCartId)
        {
            SaveData("db_product.spShoppingCart_IncrementCount", new { cartId = shoppingCartId }, "DefualtConnection");
        }

        public void DeleteShoppingCartsByUserId(string userId)
        {
            SaveData("db_product.spShoppingCart_DeleteByUserId", new { UserId = userId }, "DefualtConnection");
        }
    }
}
