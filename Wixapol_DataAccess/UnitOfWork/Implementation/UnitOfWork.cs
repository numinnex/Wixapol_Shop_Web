using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Implementation;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.ProducentRepository.Interface;
using Wixapol_DataAccess.ProductRepository.Interfaces;
using Wixapol_DataAccess.ShoppingCartRepository.Interfaces;
using Wixapol_DataAccess.SpecificationRepository.Interfaces;
using Wixapol_DataAccess.UnitOfWork.Interface;

namespace Wixapol_DataAccess.UnitOfWork.Implementation
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public ICategoryRepository Category { get; private set; }
        public IProducentRepository Producent { get; private set; }
        public ISpecificationRepository Specification { get; private set; }
        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

        public UnitOfWork(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;

            Product = new ProductRepository.Implementation.ProductRepository(config, mapper);
            Specification = new SpecificationRepository.Implementation.SpecificationRepository(config);
            Producent = new ProducentRepository.Implementation.ProducentRepository(config);
            Category = new CategoryRepository.Implementation.CategoryRepository(config);
            ShoppingCart = new ShoppingCartRepository.Implementation.ShoppingCartRespository(config);
        }
    }
}
