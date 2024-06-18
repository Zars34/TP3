using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    public class Fin
    {
        public double RND;
        public double tiempo;
        public double ACTiempoAtencion;

        //Porcentaje
        public double PRCOcupacion;

        //Contiene las horas del fin de atencion de cada servidor
        public List<double> finAtencion;

        public int media;
        public List<ClienteTemporal> clienteTemporal;
    }
}
