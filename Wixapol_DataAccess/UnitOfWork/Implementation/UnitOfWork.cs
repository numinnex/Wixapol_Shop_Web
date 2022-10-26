using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Implementation;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.ProducentRepository.Interface;
using Wixapol_DataAccess.UnitOfWork.Interface;

namespace Wixapol_DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _config;
        public ICategoryRepository Category { get; private set; }
        public IProducentRepository Producent { get; private set; }

        public UnitOfWork(IConfiguration config)
        {
            _config = config;


            Producent = new ProducentRepository.Implementation.ProducentRepository(config);
            Category = new CategoryRepository.Implementation.CategoryRepository(config);
        }
    }
}
