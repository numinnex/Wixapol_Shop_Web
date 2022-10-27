using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.DTO;

namespace Wixapol_DataAccess.MappingHelpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
