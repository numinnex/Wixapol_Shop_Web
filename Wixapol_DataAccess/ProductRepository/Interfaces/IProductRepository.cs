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
        public List<Product> GetAllByNamePattern(string pattern);
        public List<Product> GetAllByCategory(int categoryId);
        public List<Product> GetAll();
        public Product GetById(int? id);
    }
}
