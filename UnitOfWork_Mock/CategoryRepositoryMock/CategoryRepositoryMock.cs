using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork_Mock.MockDB;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.Models;

namespace UnitOfWork_Mock.CategoryRepositoryMock
{
    public class CategoryRepositoryMock : ICategoryRepository
    {
        public void CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return Categories.CategoryList;
        }

        public Category GetById(int? id)
        {
            return Categories.CategoryList.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
