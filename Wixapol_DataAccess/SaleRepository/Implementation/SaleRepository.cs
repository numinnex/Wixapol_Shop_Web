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
            throw new NotImplementedException();
        }

        public Sale GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSale(Sale sale)
        {
            throw new NotImplementedException();
        }
    }
}
