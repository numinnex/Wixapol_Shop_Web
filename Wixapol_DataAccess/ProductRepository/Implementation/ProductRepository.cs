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
    public class ProductRepository : Repository<Product>, IProductRepository
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

        public void DeleteProduct(int? id)
        {
            SaveData("db_product.spProduct_Delete", new { }, "DefualtConnection");
        }

        public List<Product> GetAll()
        {
            return LoadData("db_product.spProduct_GetAll", new { }, "DefualtConnection");
        }

        public Product GetById(int? id)
        {
            return LoadData("db_product.spProduct_GetById", new { Id = id }, "DefualtConnection").FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            SaveData("db_product.spProduct_Update", product, "DefualtConnection");

        }
    }
}
