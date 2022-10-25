using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.CategoryRepository.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IConfiguration _config;
        public CategoryRepository(IConfiguration config) : base(config)
        {
            _config = config;
        }
        public void CreateCategory(Category category)
        {
            SaveData<dynamic>("db_product.spCategory_InsertCategory", new { category.Name }, "DefualtConnection");
        }

        public void DeleteCategory(int? id)
        {
            SaveData<dynamic>("db_product.spCategory_DeleteCategory", new { Id = id }, "DefualtConnection");
        }

        public List<Category> GetAll()
        {
            return LoadData<dynamic>("db_product.spCategory_GetAll", new { }, "DefualtConnection");
        }
        public Category GetById(int? id)
        {
            return LoadData<dynamic>("db_product.spCategory_GetById", new { Id = id }, "DefualtConnection").First();
        }

        public void UpdateCategory(Category category)
        {
            SaveData<dynamic>("db_product.spCategory_Update", new { Id = category.Id, Name = category.Name }, "DefualtConnection");
        }
    }

}
