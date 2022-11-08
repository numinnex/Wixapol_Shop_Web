using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.ProductRepository.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int? id);
        public void DecreaseQuantity(int productId, int quantity);
        public List<Product> GetAllByNamePattern(string pattern);
        public List<Product> GetAllByCategoryAndName(int categoryId, string namePattern);
        public List<Product> GetAll();
        public Product GetById(int? id);
    }
}
