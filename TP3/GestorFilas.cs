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

        }

        //Este genera una llegada, creando el timepo del evento
        public void GenerarLlegada(int i)
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
                

        }

        //Este va a comenzar cuando llegue el tiempo de la llamada
        public void ComienzaLlegada(int i)
        {
            LlegadaEspecial();

            //Si no hay llegada especial, el cliente se crea en este momento
            //De lo contrario, se crea en la llegada especial
            if (fila1.servicioAdicional[0].tomaServicio = false)
            {
                ClienteTemporal clienteTemporal = SetObjetoTemporal("En Espera", 0);
                Cola(clienteTemporal, i);
            }

            //El fin de la llegada normal se realiza lo mismo
            GenerarFin(int i);

        }

        //Revisa si al comienzo el cliente quiere realizar el servicio especial o no
        public void LlegadaEspecial()
        {
                Random random = new Random();

                // Generar un número decimal aleatorio entre 0.01 y 0.99
                double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                // Redondear a dos decimales
                numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                //Usamos el indice 0 porque es el que se utiliza para la llegada (el 1 se refiere a la salida que se hace luego)
                fila1.servicioAdicional[0].RND = numeroDecimalAleatorio;

                if (fila1.servicioAdicional[0].RND < 0.18)
                {
                    fila1.servicioAdicional[0].tomaServicio = true;
                    ClienteTemporal clienteTemporal = SetObjetoTemporal("En Espera", 0);
                    Cola(clienteTemporal, i);
                GenerarFinServicioEspecial(ClienteTemporal clienteTemporal);
                }
                else
                {
                    fila1.servicioAdicional[0].tomaServicio = false;
                }
            
        }

        public void GenerarFinServicioEspecial(ClienteTemporal clienteTemporal)
        {
            for(int i = 0, i < fila1.fin[6].finAtencion.Count, i++)
            {
                if (fila1.fin[6].finAtencion[i] == 0)
                {
                    int index = fila1.estadoClientes.IndexOf(clienteTemporal);
                    fila1.estadoClientes[index].estado = atendido;
                    fila1.estadoClientes[index].inicioAtencion = fila1.reloj;
                   

                    Random random = new Random();

                    // Generar un número decimal aleatorio entre 0.01 y 0.99
                    double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                    // Redondear a dos decimales
                    numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                    fila1.fin[6].RND = numeroDecimalAleatorio;
                    fila1.fin[6].tiempo = -fila1.fin[i].media * Math.Log(1 - numeroDecimalAleatorio);
                    fila1.fin[6].finAtencion = fila1.reloj + fila1.fin[6].tiempo;
                    fila1.fin[6].ACTiempoAtencion += fila1.fin[6].tiempo;
                    fila1.fin[6].PRCOcupacion = (fila1.fin[6].ACTiempoAtencion / fila1.reloj) * 100;
                }
            }
            


        }

        public void ComienzoFinEspecial()
        {
            ClienteTemporal clienteTemporal = SetObjetoTemporal("En Espera", 0);
            //Esta cola es especial para Servicios Adicionales
            Cola(clienteTemporal, 6);
        }

        


        //Revisa si al final el cliente quiere realizar el servicio especial o no
        public void FinEspecial()
        {
            Random random = new Random();

            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            fila1.servicioAdicional[1].RND = numeroDecimalAleatorio;

            if (fila1.servicioAdicional[1].RND < 0.33)
            {
                fila1.servicioAdicional[1].tomaServicio = true;

            }
            else
            {
                fila1.servicioAdicional[1].tomaServicio = false;
            }
        }


        //Aqui creamos al objeto temporal 
        public ClienteTemporal SetObjetoTemporal(string estado, double inicioAtencion)
        {
            ClienteTemporal clienteTemporal = new ClienteTemporal(estado, inicioAtencion);

            fila1.estadoClientes.Add(clienteTemporal);

            return clienteTemporal;
         }

        public void Cola(ClienteTemporal clienteTemporal, int i)
        {
            /*******************************************************************************************

            El tiempo de espera solo se acumula cuando un objeto empieza a ser atendido
            Quiero un metodo que remueva al clienteTemporal, pero que no afecte al codigo en caso de que la cola esté 
            vacia, significando que clienteTemporal nunca estuvo en la cola

            ******************************************************************************************/

            
            //En caso de que el objeto nunca haya formado aprte de la cola, entonces el resultado será 0
            fila1.cola[i].tiempoEspera += fila1.reloj - clienteTemporal.inicioAtencion;
            fila1.cola[i].PRCtiempoFuera = (fila1.cola[i].tiempoEspera / fila1.reloj) * 100;
        }


        public void GenerarFin(int i, ClienteTemporal clienteTemporal)
        {
            //Va a buscar si uno de los servicios esta desocupado
            for(int j = 0; j < fila1.fin[i].finAtencion.Count; j++)
            {
                if (fila1.fin[i].finAtencion[j] == 0)
                {
                    clienteTemporal.estado = "Atendiendo";
                    clienteTemporal.inicioAtencion = fila1.reloj;

                    fila1.fin[i].clienteTemporal = clienteTemporal;

                    //Modifica el objeto temporal al que hace referencia
                    int index = fila1.estadoClientes.IndexOf(clienteTemporal);
                    fila1.estadoClientes[index].estado = clienteTemporal.estado;
                    fila1.estadoClientes[index].inicioAtencion = clienteTemporal.inicioAtencion;

                    Random random = new Random();

                    // Generar un número decimal aleatorio entre 0.01 y 0.99
                    double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                    // Redondear a dos decimales
                    numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                    fila1.fin[i].RND = numeroDecimalAleatorio;
                    fila1.fin[i].tiempo = -fila1.fin[i].media * Math.Log(1 - numeroDecimalAleatorio);
                    fila1.fin[i].finAtencion[j] = fila1.reloj + fila1.fin[i].tiempo;
                    fila1.fin[i].ACTiempoAtencion += fila1.fin[i].tiempo;
                    fila1.fin[i].PRCOcupacion = (fila1.fin[i].ACTiempoAtencion / fila1.reloj) * 100;
                    return;
                }
            }

        }

        //Esta funcion determina el comienzo del evento fin
        public void ComienzaFin(int i)
        {
            //Va a buscar entre los clientes temporales aquel que pertenezca al fin que comienza
            fila1.estadoClientes.Remove(fila1.fin[i].clienteTemporal);

            /***********************************************************************************
             
             Aqui es donde se revisaria si el cliente quiere revisar el servicio adicional o no

             ************************************************************************************/
        }


    }

        

    
}
