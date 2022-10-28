using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.Abstractions;
using Wixapol_DataAccess.SpecificationRepository.Interfaces;

namespace Wixapol_DataAccess.SpecificationRepository.Implementation
{
    public class SpecificationRepository : Repository<ISpecification>, ISpecificationRepository
    {
        private readonly IConfiguration _config;
        public SpecificationRepository(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public int CreateSpecification(ISpecification specification, Specification specType)
        {
            switch (specType)
            {
                case Specification.Basic:
                    return SaveDataWithReturn<dynamic>("db_product.spSpecification_AddBasicSpecification", new
                    {
                        SpecName = (specification as BaseSpecification)!.SpecName,
                        SpecValue = (specification as BaseSpecification)!.SpecValue,
                    }, "DefualtConnection");
                case Specification.Advanced:
                    return SaveDataWithReturn<dynamic>("db_product.spSpecification_AddAdvancedSpecification", new
                    {
                        SpecName = (specification as AdvancedSpecification)!.SpecName,
                        SpecValue = (specification as AdvancedSpecification)!.SpecValue,
                    }, "DefualtConnection");
                case Specification.Physical:
                    return SaveDataWithReturn<dynamic>("db_product.spSpecification_AddPhysicalSpecification", new
                    {
                        SpecName = (specification as PhysicalSpecification)!.SpecName,
                        SpecValue = (specification as PhysicalSpecification)!.SpecValue,
                    }, "DefualtConnection");
                default:
                    throw new ArgumentException("Invalid Specification Type!");
            }
        }

        public void DeleteSpecification(int? id, Specification specType)
        {
            switch (specType)
            {
                case Specification.Basic:
                    SaveData<dynamic>("db_product.spSpecification_DeleteBaseSpecification", new { Id = id }, "DefualtConnection");
                    break;
                case Specification.Advanced:
                    SaveData<dynamic>("db_product.spSpecification_DeleteAdvancedSpecification", new { Id = id }, "DefualtConnection");
                    break;
                case Specification.Physical:
                    SaveData<dynamic>("db_product.spSpecification_DeletePhysicalSpecification", new { Id = id }, "DefualtConnection");
                    break;
                default:
                    throw new ArgumentException("Invalid Specification Type!");
            }

        }

        public List<ISpecification> GetAll(Specification specType)
        {
            switch (specType)
            {
                case Specification.Basic:
                    return LoadData("db_product.spSpecification_BaseSpecificationGetAll", new { }, "DefualtConnection");
                case Specification.Advanced:
                    return LoadData("db_product.spSpecification_AdvancedSpecificationGetAll", new { }, "DefualtConnection");
                case Specification.Physical:
                    return LoadData("db_product.spSpecification_PhysicalSpecificationGetAll", new { }, "DefualtConnection");
                default:
                    throw new ArgumentException("Invalid Specification Type!");
            }
        }

        public ISpecification GetById(int? id, Specification specType)
        {
            switch (specType)
            {
                case Specification.Basic:
                    return LoadDataWithParams<dynamic, BaseSpecification>("db_product.spSpecification_BaseSpecificationGetById", new { Id = id }, "DefualtConnection").FirstOrDefault();
                case Specification.Advanced:
                    return LoadDataWithParams<dynamic, AdvancedSpecification>("db_product.spSpecification_AdvancedSpecificationGetById", new { Id = id }, "DefualtConnection").FirstOrDefault();
                case Specification.Physical:
                    return LoadDataWithParams<dynamic, PhysicalSpecification>("db_product.spSpecification_PhysicalSpecificationGetById", new { Id = id }, "DefualtConnection").FirstOrDefault();
                default:
                    throw new ArgumentException("Invalid Specification Type!");
            }
        }

        public void UpdateSpcification(ISpecification specification, Specification specType)
        {
            switch (specType)
            {
                case Specification.Basic:
                    SaveData<dynamic>("db_product.spSpecification_UpdateBaseSpecification", new
                    {
                        Id = (specification as BaseSpecification)!.Id,
                        SpecName = (specification as BaseSpecification)!.SpecName,
                        SpecValue = (specification as BaseSpecification)!.SpecValue,
                    }, "DefualtConnection");
                    break;
                case Specification.Advanced:
                    SaveData<dynamic>("db_product.spSpecification_UpdateAdvancedSpecification", new
                    {
                        Id = (specification as AdvancedSpecification)!.Id,
                        SpecName = (specification as AdvancedSpecification)!.SpecName,
                        SpecValue = (specification as AdvancedSpecification)!.SpecValue,

                    }, "DefualtConnection");
                    break;
                case Specification.Physical:
                    SaveData<dynamic>("db_product.spSpecification_UpdatePhysicalSpecification", new
                    {
                        Id = (specification as PhysicalSpecification)!.Id,
                        SpecName = (specification as PhysicalSpecification)!.SpecName,
                        SpecValue = (specification as PhysicalSpecification)!.SpecValue,

                    }, "DefualtConnection");
                    break;
                default:
                    throw new ArgumentException("Invalid Specification Type!");
            }
        }
    }
}
