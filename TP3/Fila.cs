using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    internal class Fila
    {
        public string evento;
        public float reloj;

        //Llegadas
        public List<Llegada> llegada;
        public List<ServicioAdicional> servicioAdicional;

        public Llegada clienteCaja;
        public Llegada clienteAtencionPersonalizada;
        public Llegada clienteTarjetaCredito;
        public Llegada clientePlazosFijos;
        public Llegada clientePrestamos;
        public ServicioAdicional paraLosQueLlegan;
        public ServicioAdicional paraLosQueSalen;

        //Colas
        public List<Cola> cola;

        public Cola paraCaja;
        public Cola paraAtencionPers;
        public Cola paraTarjetaCredito;
        public Cola paraPlazosFijos;
        public Cola paraPrestamos;
        public Cola paraServAdicional;

        //Fin
        public List<Fin> fin;

        public Fin cajas;
        public Fin atencionPersonalizada;
        public Fin tarjetaCredito;
        public Fin plazosFijos;
        public Fin prestamos;
        public Fin servAdicional;

        //Estados
        public List<List<string>> estados;

        public List<string> estadoCajas;
        public List<string> estadoAtencionPers;
        public List<string> estadoTarjetaCredito;
        public List<string> estadoPlazoFijo;
        public List<string> estadoPrestamos;
        public List<string> estadoServicioAdicional;

        //Objetos temporales
        public List<ClienteTemporal> estadoClientes;


        public void Initialize()
        {
            llegada = new List<Llegada>{clienteCaja,clienteAtencionPersonalizada,clienteTarjetaCredito,clientePlazosFijos,clientePrestamos};

            servicioAdicional = new List<ServicioAdicional>{paraLosQueLlegan,paraLosQueSalen};

            cola = new List<Cola>{paraCaja,paraAtencionPers,paraTarjetaCredito,paraPlazosFijos,paraPrestamos,paraServAdicional};

            fin = new List<Fin>{cajas,atencionPersonalizada,tarjetaCredito,plazosFijos,prestamos,servAdicional};

            estados = new List<List<string>>{estadoCajas,estadoAtencionPers,estadoTarjetaCredito,estadoPlazoFijo,estadoPrestamos,estadoServicioAdicional};
            
            List<int> cantObjetos = new List<int>(){4, 3, 2, 1, 2, 2 };    
            for(int i = 0; i < estados.Count; i++)
            {
                for(int j = 0; j < cantObjetos[i]; j++)
                {
                    estados[i].Add("Libre");
                }
            }


            List<int> mediasLlegada = new List<int>{2,5,10,15,6};
            for (int i = 0; i < mediasLlegada.Count; i++)
            {
                llegada[i].media = mediasLlegada[i];
            }

            List<int> mediasFin = new List<int> {6, 12, 20, 30, 15, 3}
            for(int i = 0; i < mediasFin.Count; i++)
            {
                fin[i].media = mediasFin[i];
            } 

        }
    }
}
