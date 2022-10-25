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
    }
}
