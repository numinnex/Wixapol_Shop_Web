using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork_Mock.MockDB;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.ProductRepository.Interfaces;

namespace UnitOfWork_Mock.ProductRepositoryMock
{
    public class ProductRepositoryMock : IProductRepository
    {
        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DecreaseQuantity(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return Products.ProductsList;
        }

        public List<Product> GetAllByCategoryAndName(int categoryId, string namePattern)
        {
            return Products.ProductsList.Where(x => x.CategoryId == categoryId && x.Name == namePattern).ToList();
        }

        public List<Product> GetAllByNamePattern(string pattern)
        {
            return Products.ProductsList.Where(x => x.Name == pattern).ToList();
        }

        public Product GetById(int? id)
        {
            return Products.ProductsList.Where(x => x.Id == id).First();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
