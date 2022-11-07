using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.OrderRepository.Interfaces
{
    public interface IOrderRepository
    {
        public int CreateOrder(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int? id);
        public List<Order> GetAll();
        public Order GetById(int? id);

    }
}
