using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagamentos
{
    public class Produto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
