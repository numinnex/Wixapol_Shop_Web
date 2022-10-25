using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.CategoryRepository.Interface;
using Wixapol_DataAccess.GenericRepository.Interfaces;

namespace Wixapol_DataAccess.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

    }
}
