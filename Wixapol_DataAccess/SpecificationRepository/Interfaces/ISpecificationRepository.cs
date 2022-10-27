using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.Abstractions;

namespace Wixapol_DataAccess.SpecificationRepository.Interfaces
{
    public interface ISpecificationRepository
    {
        public int CreateSpecification(ISpecification specification, Specification specType);
        public void UpdateSpcification(ISpecification specification, Specification specType);
        public void DeleteSpecification(int? id, Specification specType);
        public List<ISpecification> GetAll(Specification specType);
        public ISpecification GetById(int? id, Specification specType);
    }
}
