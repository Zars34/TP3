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

        }
    }
}
