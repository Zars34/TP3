namespace TP3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GestorFilas gestor;

            int cantEventos = 1;

            gestor.fila1.Initialize();
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

            
            gestor.fila2.evento = proxEvento + " " + "[" + idEvento + "]";
            gestor.fila2.reloj = Math.Round(proxTiempo, 2);

            gestor.ComienzaLlegada(idEvento, gestor.fila1, gestor.fila2);


            //Hasta acá todo se realizará igual cada vez que se inicie el programa

            int cantEventos;


        }

        public void EventoCiclico(int cantEventos, Fila fila1, Fila fila2)
        {
            for (int i = 0; i < cantEventos; i++)
            {
                string proxEvento;
                int idEvento;
                double proxTiempo = 0;

                for (int j = 0; j < fila1.llegada.Count; j++)
                {
                    fila2.llegada[j] = fila1.llegada[j];
                }
                for(int j = 0; j < fila1.servicioAdicional.Count; j++)
                {
                    fila2.servicioAdicional[j] = fila1.servicioAdicional[j];
                }
                for(int j = 0; j < fila1.fin.Count; j++)
                {
                    fila2.fin[j] = fila1.fin[j];
                }
            }
        }
    }
}
