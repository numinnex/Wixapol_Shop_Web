using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Interfaces;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.ProducentRepository.Interface
{
    public interface IProducentRepository
    {
        public void CreateProducent(Producent producent);
        public void UpdateProducent(Producent producent);
        public void DeleteProducent(int? id);
        public List<Producent> GetAll();
        public Producent GetById(int? id);
    }
}
