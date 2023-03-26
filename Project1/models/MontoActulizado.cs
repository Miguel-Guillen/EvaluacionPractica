using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.models
{
    internal class MontoActulizado
    {
        public int cliente { get; set; }
        public decimal monto { get; set; }

        public MontoActulizado(int cuenta, decimal montoActual)
        {
            cliente = cuenta;
            monto = montoActual;
        }
    }
}
