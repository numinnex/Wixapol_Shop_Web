using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models.Abstractions;
using Wixapol_DataAccess.SpecificationRepository.Interfaces;

namespace UnitOfWork_Mock.SpecificationRepositoryMock
{
    public class SpecificationRepositoryMock : ISpecificationRepository
    {
        public int CreateSpecification(ISpecification specification, Specification specType)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecification(int? id, Specification specType)
        {
            throw new NotImplementedException();
        }

        public List<ISpecification> GetAll(Specification specType)
        {
            throw new NotImplementedException();
        }

        public ISpecification GetById(int? id, Specification specType)
        {
            throw new NotImplementedException();
        }

        public void UpdateSpecification(ISpecification specification, Specification specType)
        {
            throw new NotImplementedException();
        }
    }
}
