using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.UserRepository.Interfaces
{
    public interface IUserRepository
    {
        public void GetByID(string guid);
    }
}
