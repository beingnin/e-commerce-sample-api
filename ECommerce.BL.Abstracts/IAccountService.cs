using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Abstracts
{
    public interface IAccountService
    {
        string GetToken(string userName, string password);
    }
}
