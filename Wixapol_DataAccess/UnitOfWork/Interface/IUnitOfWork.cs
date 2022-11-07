using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.GenericRepository.Interfaces;
using Wixapol_DataAccess.OrderRepository.Interfaces;
using Wixapol_DataAccess.ProducentRepository.Interface;
using Wixapol_DataAccess.ProductRepository.Interfaces;
using Wixapol_DataAccess.SaleRepository.Interfaces;
using Wixapol_DataAccess.ShoppingCartRepository.Interfaces;
using Wixapol_DataAccess.SpecificationRepository.Interfaces;

namespace Wixapol_DataAccess.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProducentRepository Producent { get; }
        ISpecificationRepository Specification { get; }
        IProductRepository Product { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderRepository Order { get; }
        ISaleRepository Sale { get; }



    }
}
