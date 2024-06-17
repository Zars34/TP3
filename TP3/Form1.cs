namespace TP3
{
    public partial class Form1 : Form
    {
        public GestorFilas gestor;

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

            string proxEvento;
            int idEvento;
            double proxTiempo = 0;

            for(int i = 0; i < gestor.fila1.llegada.Count; i++)
            {
                //Las primeras llamadas seran generadas manualmente; el resto se generarán automaticamente
                //cuando comience un evento de llamada
                gestor.GenerarLlegada(i);
                if(proxEvento > gestor.fila1.llegada[i].proxLlegada)
                {
                    proxTiempo = proxTiempo > gestor.fila1.llegada[i].proxLlegada;
                    idEvento = i;
                    proxEvento = gestor.fila1.llegada[i].GetType().Name;
                }
                
            }

            gestor.fila1 = fila2;
            gestor.fila1.evento = proxEvento + " " + "[" + idEvento + "]";
            gestor.fila1.reloj = Math.Round(proxTiempo, 2);

            gestor.ComienzaLlegada(idEvento, fila1, fila2);


            //Hasta acá todo se realizará igual cada vez que se inicie el programa

            int cantEventos;
            int desdeEsteEvento;

            List<Fila> filasMostradas;

            EventoCiclico(cantEventos, desdeEsteEvento, gestor.fila1, gestor.fila2);
        }

        public void EventoCiclico(int cantEventos, int desdeEsteEvento, Fila fila1, Fila fila2)
        {
            if(cantEventos != 0)
            {
                gestor.fila1 = fila2;

                string proxEvento;
                int tipoEvento;
                double proxTiempo = 0;

                //Todos los eventos llegada, usamos el for para analizarlos individualmente y determinar el siguiente evento
                for (int i = 0; i < fila1.llegada.Count; i++)
                {
                    fila2.llegada[i] = fila1.llegada[i];
                    if (proxEvento > fila1.llegada[i].proxLlegada)
                    {
                        proxTiempo = proxTiempo > fila1.llegada[i].proxLlegada;
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
                        if (proxEvento > fila1.fin[i].finAtencion[j])
                        {
                            proxTiempo = proxTiempo > fila1.fin[i].finAtencion[j];
                            tipoEvento = i;
                            proxEvento = fila1.fin[i].GetType().Name;
                        }
                    }

                }

                //Todas las colas, no hace falta revisarlos uno por uno pues le pasa la lista completa
                fila2.cola = fila1.cola;
                

                //Todos los estados de objetos permanentes, no hace falta revisarlos uno por uno pues le pasa la lista completa

                fila2.estados[i] = fila1.estados[i];
                

                //Todos los objetos temporales, no hace falta revisarlos uno por uno pues le pasa la lista completa
                fila2.estadoClientes = fila1.estadoClientes;



                //Hasta acá la fila2 será casi una copia de la fila1; desde aquí en adelante se ejecutarán los eventos
                
            }

            EventoCiclico(cantEventos--, desdeEsteEvento, fila2, fila1);
        }
    }
}
