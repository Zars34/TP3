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

            //Llegada Servicio Especial
            LlegadaServicioEspecial();

            //Llegada
            Llegada();

            //Fin Servicio Especial
            FinServicioEspecial();

            //Fin
            FinLlegada();
 

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
            ClienteTemporal clienteTemporal = SetObjetoTemporal("En Espera", 0);
            Cola(clienteTemporal, i);

            GenerarFin(int i);
        }

        //Revisa si al comienzo el cliente quiere realizar el servicio especial o no
        /*public void LlegadaEspecial()
        {
                Random random = new Random();

                // Generar un número decimal aleatorio entre 0.01 y 0.99
                double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                // Redondear a dos decimales
                numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                fila1.servicioAdicional[0].RND = numeroDecimalAleatorio;

                if (fila1.servicioAdicional[0].RND < 0.18)
                {
                    fila1.servicioAdicional[0].tomaServicio = true;

                    LlegadaServicioEspecial();
                
                }
                else
                {
                    fila1.servicioAdicional[0].tomaServicio = false;
                }
            
        }*/

        /*public void LlegadaServicioEspecial()
        {
            
             

            //El 5 tiene los estados de los serviciosEspeciales
            for (int j = 0; j < fila1.estados[5].Count; j++)
            {
                //Si hay al menos uno libre, lo toma
                if (fila1.estados[5][j] == "Libre")
                {
                    fila1.estados[5][j] = "Ocupado";

                    //Este retorno es al pedo, sirve para agregarlo en la cola si esta ocupado
                    ClienteTemporal clienteTemporal = SetObjetoTemporal("Atendido", fila1.reloj);
                    Cola(clienteTemporal, 5);
                    break;
                }
                else
                {
                    //Si no hay ninguno libre, tiene que esperar
                    ClienteTemporal clienteTemporal = SetObjetoTemporal("Esperando", 0.0);

                    fila1.cola[5].cantidad.Add(clienteTemporal);

                }

            }
        }*/


        //Revisa si al final el cliente quiere realizar el servicio especial o no
        /*public void FinEspecial()
        {
            Random random = new Random();
            //aaaa

            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            fila1.servicioAdicional[1].RND = numeroDecimalAleatorio;

            if (fila1.servicioAdicional[1].RND < 0.33)
            {
                fila1.servicioAdicional[1].tomaServicio = true;

                

                //FinLlegada(5, nroColumna??);
            }
            else
            {
                fila1.servicioAdicional[1].tomaServicio = false;
            }
        }*/


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

                    Random random = new Random();

                    // Generar un número decimal aleatorio entre 0.01 y 0.99
                    double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                    // Redondear a dos decimales
                    numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                    fila1.fin[i].RND = numeroDecimalAleatorio;
                    fila1.fin[i].tiempo = -fila1.fin[i].media * Math.Log(1 - numeroDecimalAleatorio);
                    fila1.fin[i].finAtencion[j] = fila1.reloj + fila1.fin[i].tiempo;
                    fila1.fin[i].ACTiempoAtencion += fila1.fin[i].tiempo;
                    fila1.fin[i].PRCOcupacion = fila1.fin[i].ACTiempoAtencion / fila1.reloj;
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
