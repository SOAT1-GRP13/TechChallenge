using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pagamentos.AntiCorrupition
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}
