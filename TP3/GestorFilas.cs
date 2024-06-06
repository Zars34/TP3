using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    internal class GestorFilas
    {
        public Fila fila1;
        public Fila fila2;


        public void Inicio()
        {
            fila1.Initialize();
            fila1.evento = "Inicializar";
            fila1.reloj = 0;

            //Llegada
            Llegada();

            //Llegada Servicio Especial
            LlegadaEspecial();

            

        }

        public void Llegada()
        {
            for (int i = 0; i < fila1.llegada.Count; i++)
            {
                Random random = new Random();

                // Generar un número decimal aleatorio entre 0.01 y 0.99
                double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                // Redondear a dos decimales
                numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                fila1.llegada[i].RND = numeroDecimalAleatorio;
                fila1.llegada[i].tiempo = -fila1.llegada[i].media * Math.Log(1 - numeroDecimalAleatorio);
                fila1.llegada[i].proxLlegada = fila1.reloj + fila1.llegada[i].tiempo;

                //Cuando llega un objeto revisa en los estados si hay alguna cola libre
                for (int j = 0; j < fila1.estados[i].Count; j++)
                {
                    //Si hay al menos uno libre, lo toma
                    if (fila1.estados[i][j] == "Libre")
                    {
                        fila1.estados[i][j] = "Ocupado";

                        //Este retorno es al pedo, sirve para agregarlo en la cola si esta ocupado
                        ClienteTemporal clienteTemporal = SetObjetoTemporal("Atendido", fila1.reloj);
                        break;
                    }
                    else
                    {
                        //Si no hay ninguno libre, tiene que esperar
                        ClienteTemporal clienteTemporal = SetObjetoTemporal("Esperando", 0.0);

                        fila1.cola[i].cantidad.Add(clienteTemporal);
                        

                        //****************************************************************************

                        //La acumulacion y promedio del tiempo se calculan en la futura funcion Cola()
                        //vamos a hacer que cada vez que se ejecuta Cola(), revise si tiene ojbetos esperando

                        //****************************************************************************

                    }

                }
            }
        }

        public void LlegadaEspecial()
        {
            foreach (var columna in fila1.servicioAdicional)
            {
                Random random = new Random();

                // Generar un número decimal aleatorio entre 0.01 y 0.99
                double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                // Redondear a dos decimales
                numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                columna.RND = numeroDecimalAleatorio;
                if (columna.RND < 0.18)
                {
                    columna.tomaServicio = true;
                }
                else
                {
                    columna.tomaServicio = false;
                }
            }
        }

        //Aqui creamos al objeto temporal 
        public ClienteTemporal SetObjetoTemporal(string estado, double inicioAtencion)
        {
            ClienteTemporal clienteTemporal = new ClienteTemporal(estado, inicioAtencion);

            //Revisa si hay objetos temporales; si no hay ninguno, crea el primero
            if (fila1.estadoClientes == null)
            {
                fila1.estadoClientes.Add(clienteTemporal);
                return clienteTemporal;
            } else {

                //****************************************************************************

                //Si tiene objetos temporales, asi que va a revisar si alguno esta vacío (Hay que impementar que se ponga vacio una vez 
                //que muera el objeto temporal)

                //****************************************************************************

                for (int i = 0; i < fila1.estadoClientes.Count; i++)
                {
                    if (fila1.estadoClientes[i].estado == "")
                    {
                        fila1.estadoClientes[i].estado = estado;
                        fila1.estadoClientes[i].inicioAtencion = inicioAtencion;
                        return clienteTemporal;
                    }
                    
                }
                //Si tiene objetos temporales pero todos estan llenos, asi que crea uno nuevo
                fila1.estadoClientes.Add(clienteTemporal);
                return clienteTemporal;

            }
        }

        public void Cola()
        {
            
            for(int i = 0; i < fila1.cola[i]; i++)
            {
                //Cada vez que re realice la funcion Cola() vamos a reiniciar los valores de tiempo espera de cada cola
                //esto es para que siempre contenga 
                if (fila1.cola[i] != null)
                {
                    for(int j = 0; j < fila1.cola[i].Count; j++)
                    {
                        fila1.cola[i].tiempoEspera += (fila1.reloj - fila1.cola[i].cantidad[i].inicioAtencion);
                    }
                }
            }
        }

    }


    }
}
