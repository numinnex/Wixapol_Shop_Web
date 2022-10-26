using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.ProducentRepository.Interface;

namespace Wixapol_DataAccess.ProducentRepository.Implementation
{
    public class ProducentRepository : Repository<Producent>, IProducentRepository
    {
        private readonly IConfiguration _config;
        public ProducentRepository(IConfiguration config) : base(config)
        {
            _config = config;
        }
        public void CreateProducent(Producent producent)
        {
            SaveData<dynamic>("db_product.spProducent_Insert", new
            {
                Name = producent.Name,
                ProducentCode = producent.ProducentCode,
                EAN = producent.EAN
            }
            , "DefualtConnection");
        }

        public void DeleteProducent(int? id)
        {
            SaveData<dynamic>("db_product.spProducent_DeleteProducent", new { Id = id }, "DefualtConnection");
        }

        public List<Producent> GetAll()
        {
            return LoadData<dynamic>("db_product.spProducent_GetAll", new { }, "DefualtConnection");
        }
        public Producent GetById(int? id)
        {
            return LoadData<dynamic>("db_product.spProducent_GetById", new { Id = id }, "DefualtConnection").First();
        }

        public void UpdateProducent(Producent producent)
        {
            SaveData<dynamic>("db_product.spProducent_Update", new
            {
                Id = producent.Id,
                Name = producent.Name,
                ProducentCode = producent.ProducentCode,
                EAN = producent.EAN
            }, "DefualtConnection");
        }
    }

}
