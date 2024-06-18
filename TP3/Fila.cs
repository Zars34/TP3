using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    public class Fila
    {
        public string evento;
        public float reloj;

        //Llegadas
        public List<Llegada> llegada = new List<Llegada>();
        public List<ServicioAdicional> servicioAdicional;

        public Llegada clienteCaja;
        public Llegada clienteAtencionPersonalizada;
        public Llegada clienteTarjetaCredito;
        public Llegada clientePlazosFijos;
        public Llegada clientePrestamos;
        public Llegada clienteServicioEspecial;
        public ServicioAdicional paraLosQueLlegan;
        public ServicioAdicional paraLosQueSalen;

        //Colas
        public List<Cola> cola;

        /*0*/public Cola paraCaja;
        /*1*/public Cola paraAtencionPers;
        /*2*/public Cola paraTarjetaCredito;
        /*3*/public Cola paraPlazosFijos;
        /*4*/public Cola paraPrestamos;
        /*5*/public Cola paraServAdicional;

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

        public List<string> estadoCajas = new List<string> { "Libre", "Libre", "Libre", "Libre"};
        public List<string> estadoAtencionPers = new List<string> { "Libre", "Libre", "Libre" };
        public List<string> estadoTarjetaCredito = new List<string> { "Libre", "Libre" };
        public List<string> estadoPlazoFijo = new List<string> { "Libre"};
        public List<string> estadoPrestamos = new List<string> { "Libre", "Libre" };
        public List<string> estadoServicioAdicional = new List<string> { "Libre", "Libre" };

        //Objetos temporales
        public List<ClienteTemporal> estadoClientes;


        public void Initialize()
        {
            llegada = new List<Llegada>{clienteCaja,clienteAtencionPersonalizada,clienteTarjetaCredito,clientePlazosFijos,clientePrestamos,clienteServicioEspecial };

            servicioAdicional = new List<ServicioAdicional>{paraLosQueLlegan,paraLosQueSalen};

            cola = new List<Cola>{paraCaja,paraAtencionPers,paraTarjetaCredito,paraPlazosFijos,paraPrestamos,paraServAdicional};

            fin = new List<Fin>{cajas,atencionPersonalizada,tarjetaCredito,plazosFijos,prestamos,servAdicional};

            estados = new List<List<string>>{estadoCajas,estadoAtencionPers,estadoTarjetaCredito,estadoPlazoFijo,estadoPrestamos,estadoServicioAdicional};


            List<int> mediasLlegada = new List<int>{2,5,10,15,6};
            for (int i = 0; i < mediasLlegada.Count; i++)
            {
                llegada[i].media = mediasLlegada[i];
            }

            List<int> mediasFin = new List<int> { 6, 12, 20, 30, 15, 3 };
            for(int i = 0; i < mediasFin.Count; i++)
            {
                fin[i].media = mediasFin[i];
            } 

        }
    }
}
