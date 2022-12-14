using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.OrderRepository.Interfaces;

namespace Wixapol_DataAccess.OrderRepository.Implementation
{
    public sealed class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IConfiguration _config;
        public OrderRepository(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public int CreateOrder(Order order)
        {
            return SaveDataWithReturn("db_product.spOrder_Insert", order, "DefualtConnection");
        }

        public void DeleteOrder(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int? id)
        {
            return LoadData("db_product.spOrder_GetById", new { Id = id }, "DefualtConnection").FirstOrDefault();
        }

        public void UpdateOrder(Order order, int? orderId)
        {
            SaveData("db_product.spOrder_Update", new
            {
                Id = orderId,
                order.PhoneNumber,
                order.Name,
                order.Adress,
                order.Email,
                order.City,
                order.PostalCode,



            }, "DefualtConnection");
        }
    }
}
