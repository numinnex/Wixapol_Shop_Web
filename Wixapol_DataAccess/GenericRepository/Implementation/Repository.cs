using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Interfaces;

namespace Wixapol_DataAccess.GenericRepository.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IConfiguration _config;

        public Repository(IConfiguration config)
        {
            _config = config;
        }
        private string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadDataWithJoin<U, V>(string storedProcedure, Func<T, U, V, T> del, string splitValue, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query(storedProcedure, del, splitOn: splitValue, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }
        public List<T> LoadDataWithJoinParams<U, V, D>(string storedProcedure, Func<T, U, V, T> del, D parameters, string splitValue, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query(storedProcedure, del, parameters, splitOn: splitValue, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }
        public List<V> LoadDataWithParams<U, V>(string storedProcedure, U parameters, string connectionStringName)
        {

            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<V> rows = connection.Query<V>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }
        public List<T> LoadData<U>(string storedProcedure, U parameters, string connectionStringName)
        {

            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void RemoveData(string storedProcedure, int id, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, id, commandType: CommandType.StoredProcedure);
            }

        }

        public void RemoveRange<U>(string storedProcedure, U parameters, string connectionStringName)
        {

            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveData<U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int SaveDataWithReturn<U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingle<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
