using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.DTO;
using Wixapol_DataAccess.SaleRepository.Interfaces;

namespace Wixapol_DataAccess.SaleRepository.Implementation
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public SaleRepository(IConfiguration config, IMapper mapper) : base(config)
        {
            _config = config;
            _mapper = mapper;
        }

        public int CreateSale(Sale sale)
        {
            SaleDTO saleDTO = _mapper.Map<SaleDTO>(sale);
            return SaveDataWithReturn("db_product.spSale_Insert", saleDTO, "DefualtConnection");
        }

        public void DeleteSale(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAll()
        {
            return LoadData("db_product.spSale_GetAll", new { }, "DefualtConnection");
        }

        public Sale GetById(int? id)
        {
            return LoadData("db_product.spSale_GetById", new { Id = id }, "DefualtConnection").FirstOrDefault();
        }
        public Sale GetByOrderId(int? id)
        {
            return LoadData("db_product.spSale_GetByOrderId", new { Id = id }, "DefualtConnection").FirstOrDefault();
        }

        public List<Sale> GetByStatus(string status)
        {
            return LoadData("db_product.spSale_GetByOrderStatus", new { Status = status }, "DefualtConnection");
        }

        public void UpdateSale(Sale sale)
        {
            SaveData("db_product.spSale_UpdateSale", new
            {
                Id = sale.Id,
                sale.OrderStatus,
                sale.PaymentStatus,
                sale.Carrier,
                sale.TrackingNumber,
                sale.ShippingDate,

            }, "DefualtConnection");
        }

        public void UpdateSessionInformation(int id, string sessionId, string paymentIntentId)
        {
            SaveData("db_product.spSale_UpdateSessionInfo", new { Id = id, SessionId = sessionId, PaymentIntentId = paymentIntentId }, "DefualtConnection");
        }

        public void UpdateStatus(int id, string orderStatus, string paymentStatus)
        {
            SaveData("db_product.spSale_UpdateStatus", new { Id = id, OrderStatus = orderStatus, PaymentStatus = paymentStatus }, "DefualtConnection");
        }
    }
}
