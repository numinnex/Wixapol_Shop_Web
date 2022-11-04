using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.ShoppingCartRepository.Interfaces
{
    public interface IShoppingCartRepository
    {
        public void CreateShoppingCart(ShoppingCart shoppingCart);
        public List<ShoppingCart> GetShoppingCartByUserId(string id);
        public void UpdateShoppingCart(ShoppingCart shoppingCart);
        public void UpdateShoppingCartProductCount(int shoppingCartId, int count);
        public void DecrementShoppingCartProductCount(ShoppingCart shoppingCart, int count);
        public void DeleteShoppingCart(int? id);
        public List<ShoppingCart> GetAll();
    }
}
