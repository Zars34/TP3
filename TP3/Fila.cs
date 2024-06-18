using System;
using System.Collections.Generic;

namespace TP3
{
    public class Fila
    {
        public string evento = "Inicializar";
        public float reloj = 0;

        // Llegadas
        public List<Llegada> llegada;
        public List<ServicioAdicional> servicioAdicional;

        public Llegada clienteCaja = new Llegada();
        public Llegada clienteAtencionPersonalizada = new Llegada();
        public Llegada clienteTarjetaCredito = new Llegada();
        public Llegada clientePlazosFijos = new Llegada();
        public Llegada clientePrestamos = new Llegada();
        public Llegada clienteServicioEspecial = new Llegada();
        public ServicioAdicional paraLosQueLlegan = new ServicioAdicional();
        public ServicioAdicional paraLosQueSalen = new ServicioAdicional();

        // Colas
        public List<Cola> cola;

        public Cola paraCaja = new Cola();
        public Cola paraAtencionPers = new Cola();
        public Cola paraTarjetaCredito = new Cola();
        public Cola paraPlazosFijos = new Cola();
        public Cola paraPrestamos = new Cola();
        public Cola paraServAdicional = new Cola();

        // Fin
        public List<Fin> fin;

        public Fin cajas = new Fin();
        public Fin atencionPersonalizada = new Fin();
        public Fin tarjetaCredito = new Fin();
        public Fin plazosFijos = new Fin();
        public Fin prestamos = new Fin();
        public Fin servAdicional = new Fin();

        // Estados
        public List<List<string>> estados;

        public List<string> estadoCajas = new List<string>();
        public List<string> estadoAtencionPers = new List<string>();
        public List<string> estadoTarjetaCredito = new List<string>();
        public List<string> estadoPlazoFijo = new List<string>();
        public List<string> estadoPrestamos = new List<string>();
        public List<string> estadoServicioAdicional = new List<string>();

        // Objetos temporales
        public List<ClienteTemporal> estadoClientes = new List<ClienteTemporal>();

        public void Initialize()
        {
            llegada = new List<Llegada> { clienteCaja, clienteAtencionPersonalizada, clienteTarjetaCredito, clientePlazosFijos, clientePrestamos, clienteServicioEspecial };

            servicioAdicional = new List<ServicioAdicional> { paraLosQueLlegan, paraLosQueSalen };

            cola = new List<Cola> { paraCaja, paraAtencionPers, paraTarjetaCredito, paraPlazosFijos, paraPrestamos, paraServAdicional };

            fin = new List<Fin> { cajas, atencionPersonalizada, tarjetaCredito, plazosFijos, prestamos, servAdicional };

            estados = new List<List<string>> { estadoCajas, estadoAtencionPers, estadoTarjetaCredito, estadoPlazoFijo, estadoPrestamos, estadoServicioAdicional };

            List<int> cantObjetos = new List<int> { 4, 3, 2, 1, 2, 2 };
            for (int i = 0; i < estados.Count; i++)
            {
                for (int j = 0; j < cantObjetos[i]; j++)
                {
                    estados[i].Add("Libre");
                }
            }

            List<int> mediasLlegada = new List<int> { 2, 5, 10, 15, 6 };
            for (int i = 0; i < mediasLlegada.Count; i++)
            {
                llegada[i].media = mediasLlegada[i];
            }

            List<int> mediasFin = new List<int> { 6, 12, 20, 30, 15, 3 };
            for (int i = 0; i < mediasFin.Count; i++)
            {
                fin[i].media = mediasFin[i];
            }
        }
    }
}