using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Interfaces;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.CategoryRepository.Interface
{
    public interface ICategoryRepository
    {
        public void CreateCategory(Category category);
        public void UpdateCategory(Category category);
        public void DeleteCategory(int? id);
        public List<Category> GetAll();
        public Category GetById(int? id);
    }
}
