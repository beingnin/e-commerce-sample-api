using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infra.Abstracts
{
    public interface ICustomLogger
    {
        Guid Info(string message);
        Guid Error(Exception exception);
        void Initialize(string connectionString);
    }
}
