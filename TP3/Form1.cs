namespace TP3
{
    public partial class Form1 : Form
    {
        public GestorFilas gestor;

        Dictionary<int, string> llegadaMap = new Dictionary<int, string>
        {
            { 0, "LlegadaClienteCaja" },
            { 1, "LlegadaClienteAtencionPersonalizada" },
            { 2, "LlegadaClienteTarjetaCredito" },
            { 3, "LlegadaClientePlazosFijos" },
            { 4, "LlegadaClientePrestamos" },
            { 5, "LlegadaClienteServicioEspecial" }

        };

        Dictionary<int, string> finMap = new Dictionary<int, string>
        {
            { 0, "FinCaja" },
            { 1, "FinAtencionPersonalizada" },
            { 2, "FinTarjetaCredito" },
            { 3, "FinPlazoFijo" },
            { 4, "FinPrestamos" },
            { 5, "FinServicioEspecial" }

        };

        public Form1()
        {
            InitializeComponent();

            int cantEventos = 1;

            Fila fila1 = new Fila();
            fila1.Initialize();

            Fila fila2 = new Fila();
            fila2.Initialize();

            gestor.fila1 = fila1;
            gestor.fila1.evento = "Inicializar";
            gestor.fila1.reloj = 0;

            string proxEvento = "Inicializar";
            int idEvento = -1;
            double proxTiempo = 0;

            for(int i = 0; i < gestor.fila1.llegada.Count; i++)
            {
                //Las primeras llamadas seran generadas manualmente; el resto se generarán automaticamente
                //cuando comience un evento de llamada
                gestor.GenerarLlegada(i);
                if(proxTiempo > gestor.fila1.llegada[i].proxLlegada)
                {
                    proxTiempo = gestor.fila1.llegada[i].proxLlegada;
                    idEvento = i;
                    proxEvento = gestor.fila1.llegada[i].GetType().Name;
                }
                
            }

            //Dios, hay que refactorizar "fila1" en "GestorFilas" para llamarlo "Fila"
            gestor.fila1 = fila2;
            gestor.fila1.evento = proxEvento + " " + "[" + idEvento + "]";
            gestor.fila1.reloj = (float)Math.Round(proxTiempo, 2);

            gestor.ComienzaLlegada(idEvento);


            //Hasta acá todo se realizará igual cada vez que se inicie el programa

            
            int desdeEsteEvento = 0;

            List<Fila> filasMostradas;

            EventoCiclico(cantEventos, desdeEsteEvento, fila1, fila2);


        }

        public void EventoCiclico(int cantEventos, int desdeEsteEvento, Fila fila1, Fila fila2)
        {
            if(cantEventos != 0)
            {
                gestor.fila1 = fila2;

                string proxEvento;
                int tipoEvento = -1;

                //Esta variable se inicializará siempre como -1; su valor cambiará si el proximo evento es un Fin en vez de una Llegada
                int servicioFin = -1;
                double proxTiempo = 0;


                //Todos los eventos llegada, usamos el for para analizarlos individualmente y determinar el siguiente evento
                for (int i = 0; i < fila1.llegada.Count; i++)
                {
                    fila2.llegada[i] = fila1.llegada[i];
                    if (proxTiempo > fila1.llegada[i].proxLlegada)
                    {
                        proxTiempo = fila1.llegada[i].proxLlegada;
                        tipoEvento = i;
                        proxEvento = fila1.llegada[i].GetType().Name;
                    }
                }

                //Todos los servicios adicionales (si los usa o no), no hace falta revisarlos uno por uno pues le pasa la lista completa

                fila2.servicioAdicional = fila1.servicioAdicional;
                    
                

                //Todos los eventos fin, usamos el for para analizarlos individualmente y determinar el siguiente evento
                for(int i = 0; i < fila1.fin.Count; i++)
                {
                    fila2.fin[i] = fila1.fin[i];

                    for (int j = 0; j < fila1.estados[j].Count; j++)
                    {
                        if (proxTiempo > fila1.fin[i].finAtencion[j])
                        {
                            proxTiempo = fila1.fin[i].finAtencion[j];
                            tipoEvento = i;
                            servicioFin = j;
                            proxEvento = fila1.fin[i].GetType().Name;
                        }
                    }

                }

                //Todas las colas, no hace falta revisarlos uno por uno pues le pasa la lista completa
                fila2.cola = fila1.cola;
                

                //Todos los estados de objetos permanentes, no hace falta revisarlos uno por uno pues le pasa la lista completa

                fila2.estados = fila1.estados;
                

                //Todos los objetos temporales, no hace falta revisarlos uno por uno pues le pasa la lista completa
                fila2.estadoClientes = fila1.estadoClientes;



                //Hasta acá la fila2 será casi una copia de la fila1; desde aquí en adelante se ejecutarán los eventos
                fila2.reloj = (float)Math.Round(proxTiempo, 2);

                if (servicioFin == -1)
                {
                    llegadaMap.TryGetValue(tipoEvento, out string value);
                    fila2.evento = value;
                    gestor.ComienzaLlegada(tipoEvento);
                } else
                {
                    finMap.TryGetValue(tipoEvento, out string value);
                    fila2.evento = value;
                        gestor.ComienzaFin(tipoEvento, servicioFin);    
                }
                

            }


            EventoCiclico(cantEventos--, desdeEsteEvento, fila2, fila1);
        }
    }
}
