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
        public int id;
        
        public ClienteTemporal(string estado, double inicioAtencion, int id)
        {
            this.estado = estado;   
            this.inicioAtencion = inicioAtencion;
            this.id = id;
        }
    }
}
