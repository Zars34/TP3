using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    public class ClienteTemporal
    {
        public string estado;
        public double inicioAtencion;
        public int id;
        public int tipoServicio;
        public bool tomaServicio;
        //Lo sé, es muy sucio, pero necesito esta variable para una unica cosa:
        /*
         Cuando se ejecuta un fin de evento es posible que alguien vaya a un servicio especial; el problema es que 
        ese servicio especial tambien genera el mismo evento de fin, solo que con el index del Servicio Especial, lo que hace
        que vuelva a pasar por la misma funcion que le preguntó antes si queria hacer o no el servicio especial
         */
        public bool yaEligio;

        /*
          0 - 
          1 - 
          2 - 
          3 - 
          4 - 
          5 - 
         */
        
        public ClienteTemporal(string estado, double inicioAtencion, int id, int tipoServicio, bool tomaServicio, bool yaEligio)
        {
            this.estado = estado;   
            this.inicioAtencion = inicioAtencion;
            this.id = id;
            this.tipoServicio = tipoServicio;
            this.tomaServicio = tomaServicio;
            this.yaEligio = yaEligio;
        }
    }
}
