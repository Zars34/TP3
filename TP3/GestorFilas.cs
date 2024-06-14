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
        public Random random;

        /*
         0 - Caja;
         1 - AtencionPersonalizada;
         2 - TarjetaCredito;
         3 - PlazosFijos;
         4 - Prestamos;
         5 - Servicio especial (5 - LosQueLlegan
                                6 - LosQueSeVan)
         */


        public void Inicio()
        {
            fila1.Initialize();
            fila1.evento = "Inicializar";
            fila1.reloj = 0;


            /*
             Entonces, por cada fila el orden seria:

            - GenerarLlegada(numero de evento): Genera una nueva llegada

            - ComienzaLlegada(numero de evento, que seria el que gane el reloj): Revisa si realiza una llegada
            especial; se genera el objeto sin importar, se realiza o no la llegada especial, luego pasa o vuelve a la llegada normal
            que genera un fin

            - ComienzoFin(numero de evento, que seria el que gane el reloj): Comienza el fin del evento ganador, debe borrar al
            objeto temporal, revisar la cola por si hay otro objeto esperando y cambiar o no el estado del objeto permanente
            

             */

        }

        //Genera al llamada; el parametro indica el tipo de llamada
        public void GenerarLlegada(int i)
        {
                // Generar un número decimal aleatorio entre 0.01 y 0.99
                double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                // Redondear a dos decimales
                numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                fila1.llegada[i].RND = numeroDecimalAleatorio;
                fila1.llegada[i].tiempo = -fila1.llegada[i].media * Math.Log(1 - numeroDecimalAleatorio);
                fila1.llegada[i].proxLlegada = fila1.reloj + fila1.llegada[i].tiempo;

                //Cuando llega un objeto revisa en los estados si hay alguna cola libre
                

        }

        //Comienza la llamada cuando llega el evento, el parametro es el tipo de llamada
        public void ComienzaLlegada(int i)
        {
            //El 0 indica que aun no ha sido atendido
            ClienteTemporal clienteTemporal = SetObjetoTemporal("En Espera", 0, random.Next(1, 10000);

            //Crear cliente antes; enviarlo a la llegadaEspecial
            LlegadaEspecial(clienteTemporal);

            //Si no hay llegada especial, el cliente se crea en este momento
            //De lo contrario, se crea en la llegada especial
            

            //El fin de la llegada normal se realiza lo mismo
            GenerarFin(int i, ClienteTemporal clienteTemporal);

        }

        //Revisa si al comienzo el cliente quiere realizar el servicio especial o no
        public void LlegadaEspecial(ClienteTemporal clienteTemporal)
        {
                // Generar un número decimal aleatorio entre 0.01 y 0.99
                double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

                // Redondear a dos decimales
                numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

                //Usamos el indice 0 porque es el que se utiliza para la llegada (el 1 se refiere a la salida que se hace luego)
                fila1.servicioAdicional[0].RND = numeroDecimalAleatorio;

                if (fila1.servicioAdicional[0].RND < 0.18)
                {
                    fila1.servicioAdicional[0].tomaServicio = true;
                    GenerarFinServicioEspecial(ClienteTemporal clienteTemporal);
                    return;
                }
                else
                {
                    fila1.servicioAdicional[0].tomaServicio = false;
                }
            
        }

        public void GenerarFinServicioEspecial(ClienteTemporal clienteTemporal)
        {


            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            //El indice 6 indica que es un fin de ServicioAdicional
            fila1.fin[6].RND = numeroDecimalAleatorio;
            fila1.fin[6].tiempo = -fila1.fin[6].media * Math.Log(1 - numeroDecimalAleatorio);
            fila1.fin[6].finAtencion = fila1.reloj + fila1.fin[6].tiempo;
            

        }

        public void FinServicioEspecial(ClienteTemporal clienteTemporal)
        {
            for (int i = 0, i < fila1.fin[6].finAtencion.Count, i++)
            {
                if (fila1.fin[6].finAtencion[i] == 0)
                {
                    int index = fila1.estadoClientes.IndexOf(clienteTemporal);
                    fila1.estadoClientes[index].estado = atendido;
                    fila1.estadoClientes[index].inicioAtencion = fila1.reloj;


                    fila1.fin[6].ACTiempoAtencion += fila1.fin[6].tiempo;
                    fila1.fin[6].PRCOcupacion = (fila1.fin[6].ACTiempoAtencion / fila1.reloj) * 100;
                }
            }

            /****************************************************************************************
             Este tiene que generar el fin normal de la llegada de la que proviene; para eso le va a enviar el 
            cliente que ha usado hasta ahora, el cual contiene el tipo de llegada al que pertenece
             *****************************************************************************************/
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

            /************************************************************
             
             Es practicamente lo mismo que LlegadaEspecial, solo que ocurre mas adelante y que puede borrar el objeto una vez termina

             ***************************************************************/
        }


        //Aqui creamos al objeto temporal 
        public ClienteTemporal SetObjetoTemporal(string estado, double inicioAtencion, int id, int tipoServicio)
        {
            ClienteTemporal clienteTemporal = new ClienteTemporal(estado, inicioAtencion, id, tipoServicio);

            //Aprovechamos y la agregamos a la lista de objetos temporales
            fila1.estadoClientes.Add(clienteTemporal);

            return clienteTemporal;
         }

        //Este metodo se hace para calcular el tiempo de espera y porcentaje de cada objeto temporal
        //Se activa cada vez que un objeto egresa de la cola
        public void Cola(ClienteTemporal clienteTemporal, int i)
        {   
            //En caso de que el objeto nunca haya formado aprte de la cola, entonces el resultado será 0
            fila1.cola[i].tiempoEspera += fila1.reloj - clienteTemporal.inicioAtencion;
            fila1.cola[i].PRCtiempoFuera = (fila1.cola[i].tiempoEspera / fila1.reloj) * 100;
        }

        //Genera el fin, se le ingresa el tipo de fin y el clienteTemporal al que esta asignado
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
                    //No lo busco con el id del cliente temporal porque esta forma es mas rapida, ademas de que no cambia

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

            //Si ninguno de los objetos permanentes esta disponible, entonces agrega al objeto temporal a la cola que le corresponde
            fila1.cola[i].cantidad.Add(clienteTemporal);
        }

        //Es el comienzo del fin, se le envia el tipo de fin
        public void ComienzaFin(int i)
        {

            /***********************************************************************************
             
             Aqui es donde se revisaria si el cliente quiere revisar el servicio adicional o no

             ************************************************************************************/


            //Va a buscar entre los clientes temporales aquel que pertenezca al fin que comienza
            fila1.estadoClientes.Remove(fila1.fin[i].clienteTemporal);



            //Revisa si hay algun objeto temporal en la cola, para comenzar con su procesamiento
            if (fila1.cola[i].cantidad.Count != 0)
            {
                //Revisa todos los clientes temporales
                foreach(var cliente in fila1.estadoClientes)
                {

                    //Busca aquel cuyo id sea igual al del primero de la cola (el primero que entró)
                    if(cliente.id == fila1.cola[i].cantidad[0].id)
                    {
                        //Cambia el estado del objeto temporal de "En espera" a "Atendido"
                        //RECORDAR, EL OBJETO YA EXISTIA, SOLO ESTABA "En espera"
                        cliente.estado = "Atendido";
                        cliente.inicioAtencion = fila1.reloj;
                        
                        ClienteTemporal clienteCola = fila1.cola[i].cantidad[0];

                        Cola(clienteCola, i);

                        //Genero el nuevo fin del mismo tipo y con el ClienteTemporal de la cola
                        GenerarFin(int i, clienteCola);

                        fila1.cola[i].cantidad.RemoveAt(0);
                        return;
                    }
                }
            }

            
        }


    }

        

    
}
