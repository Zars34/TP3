using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    internal class ClienteTemporal
    {
        public string estado;
        public double inicioAtencion;
        
        public ClienteTemporal(string estado, double inicioAtencion)
        {
            this.estado = estado;   
            this.inicioAtencion = inicioAtencion;
        }
    }
}
