using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.GenericRepository.Implementation
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(string storedProcedure, string connectionStringName);
        void SaveData(string storedProcedure, T parameters, string connectionStringName);
        void RemoveData(string storedProcedure, int id, string connectionStringName);
    }
}
