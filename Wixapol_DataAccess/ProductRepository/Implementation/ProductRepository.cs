using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.DTO;
using Wixapol_DataAccess.ProductRepository.Interfaces;

namespace Wixapol_DataAccess.ProductRepository.Implementation
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {


        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public ProductRepository(IConfiguration config, IMapper mapper) : base(config)
        {
            _config = config;
            _mapper = mapper;
        }
        public void CreateProduct(Product product)
        {
            var productDTO = _mapper.Map<ProductDTO>(product);

            SaveData("db_product.spProduct_Insert", productDTO, "DefualtConnection");
        }

        public void DecreaseQuantity(int productId, int quantity)
        {
            SaveData("db_product.spProduct_DecreaseQuantity", new { Id = productId, Quantity = quantity }, "DefualtConnection");

        }

        public void DeleteProduct(int? id)
        {
            SaveData("db_product.spProduct_Delete", new { Id = id }, "DefualtConnection");
        }

        public List<Product> GetAll()
        {
            return LoadDataWithJoin<Category, Producent>("db_product.spProduct_GetAll", (Product, Category, Producent) =>
            {
                Product.Category = Category;
                //Product.CategoryId = Category.Id;

                Product.Producent = Producent;
                //Product.ProducentId = Producent.Id;

                return Product;

            }, "CategoryId,ProducentId", "DefualtConnection");
        }
        public List<Product> GetAllByCategoryAndName(int categoryId, string namePattern)
        {
            return LoadDataWithJoinParams<Category, Producent, dynamic>("db_product.spProduct_GetByCategory", (Product, Category, Producent) =>
            {
                Product.Category = Category;
                //Product.CategoryId = Category.Id;

                Product.Producent = Producent;
                //Product.ProducentId = Producent.Id;

                return Product;

            }, new { CategoryId = categoryId, NamePattern = namePattern }, "CategoryId,ProducentId", "DefualtConnection");
        }
        public List<Product> GetAllByNamePattern(string pattern)
        {
            return LoadDataWithJoinParams<Category, Producent, dynamic>("db_product.spProduct_GetByNamePattern", (Product, Category, Producent) =>
            {
                Product.Category = Category;
                //Product.CategoryId = Category.Id;

                Product.Producent = Producent;
                //Product.ProducentId = Producent.Id;

                return Product;

            }, new { NamePattern = pattern }, "CategoryId,ProducentId", "DefualtConnection");
        }

        public Product GetById(int? id)
        {
            return LoadDataWithJoinParams<Category, Producent, dynamic>("db_product.spProduct_GetById", (Product, Category, Producent) =>
            {
                Product.Category = Category;
                //Product.CategoryId = Category.Id;

                Product.Producent = Producent;
                //Product.ProducentId = Producent.Id;

                return Product;

            }, new { Id = id }, "CategoryId,ProducentId", "DefualtConnection").First();
        }

        public void UpdateProduct(Product product)
        {


            var productDTO = _mapper.Map<ProductDTOWithId>(product);
            SaveData("db_product.spProduct_Update", productDTO, "DefualtConnection");

        }
    }
}
