using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.GenericRepository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> LoadData<U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveData<U>(string storedProcedure, U parameters, string connectionStringName);
        void RemoveData(string storedProcedure, int id, string connectionStringName);
        void RemoveRange<U>(string storedProcedure, U parameters, string connectionStringName);
    }
}
