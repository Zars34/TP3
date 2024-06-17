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
         5 - Servicio especial 
         */

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
            ClienteTemporal clienteTemporal = SetObjetoTemporal("En Espera", 0, random.Next(1, 10000), i, false, false);

            //Crear cliente antes; enviarlo a la llegadaEspecial
            LlegadaEspecial(clienteTemporal);

            //Si no hay llegada especial, el cliente se crea en este momento
            //De lo contrario, se crea en la llegada especial

            if (clienteTemporal.tomaServicio == false)
            {
                GenerarFin(int i, ClienteTemporal clienteTemporal);
            }

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
                    clienteTemporal.tomaServicio = true;
                    GenerarFinServicioEspecial(ClienteTemporal clienteTemporal);
                    return;
                }
                else
                {
                    clienteTemporal.tomaServicio = false;
                }
            
        }

        public void GenerarFinServicioEspecial(ClienteTemporal clienteTemporal)
        {
            //primero revisa los estados; si alguno esta libre, pasa a estar ocupado; sino, va a la cola
            for (int j = 0; j < fila1.estados[5].Count; j++)
            {
                if (fila1.estados[5][j] == "Libre")
                {
                    fila1.estados[5][j] = "Ocupado";
                    clienteTemporal.estado = "Atendiendo";
                    clienteTemporal.inicioAtencion = fila1.reloj;

                    fila1.fin[5].clienteTemporal[j] = clienteTemporal;

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

                    fila1.fin[5].RND = numeroDecimalAleatorio;
                    fila1.fin[5].tiempo = -fila1.fin[i].media * Math.Log(1 - numeroDecimalAleatorio);
                    fila1.fin[5].finAtencion[j] = fila1.reloj + fila1.fin[i].tiempo;
                    fila1.fin[5].ACTiempoAtencion += fila1.fin[i].tiempo;
                    fila1.fin[5].PRCOcupacion = (fila1.fin[i].ACTiempoAtencion / fila1.reloj) * 100;

                    Cola(clienteTemporal);
                    return;
                }
            }

            fila1.cola[5].cantidad.Add(clienteTemporal);

        }


        //Sacamos "servidorFin" de la posicion del tiempo que haya ganado en el reloj
        public void FinServicioEspecial(int servidorFin)
        {
            if (fila1.cola[5].cantidad.Count != 0)
            {
                foreach (var cliente in fila1.estadoClientes)
                {

                    //Busca aquel cuyo id sea igual al del primero de la cola (el primero que entró)
                    if (cliente.id == fila1.cola[5].cantidad[0].id)
                    {
                        //Cambia el estado del objeto temporal de "En espera" a "Atendido"
                        //RECORDAR, EL OBJETO YA EXISTIA, SOLO ESTABA "En espera"
                        cliente.estado = "Atendido";
                        cliente.inicioAtencion = fila1.reloj;

                        ClienteTemporal clienteCola = fila1.cola[5].cantidad[0];

                        //Genero el nuevo fin del mismo tipo y con el ClienteTemporal de la cola
                        GenerarFin(5, clienteCola);

                        fila1.cola[5].cantidad.RemoveAt(0);
                        return;
                    }
                }
            }

            GenerarFin(fila1.fin[5].clienteTemporal[servidorFin].tipoServicio, fila1.fin[5].clienteTemporal[servidorFin]);

        }


        //Revisa si al final el cliente quiere realizar el servicio especial o no
        public void FinEspecial(ClienteTemporal clienteTemporal)
        {
            clienteTemporal.yaEligio = true;
            //Inicializa como false, especificamente para el caso del final, donde puede enredarse con el "true"
            //de cuando realizó la LlegadaEspecial
            clienteTemporal.tomaServicio = false;

            Random random = new Random();

            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            fila1.servicioAdicional[1].RND = numeroDecimalAleatorio;

            if (fila1.servicioAdicional[1].RND < 0.33)
            {
                clienteTemporal.tomaServicio = true;
                GenerarFinServicioEspecial(ClienteTemporal clienteTemporal);
                return;
            }
            else
            {
                clienteTemporal.tomaServicio = false;
            }

        }


        //Aqui creamos al objeto temporal 
        public ClienteTemporal SetObjetoTemporal(string estado, double inicioAtencion, int id, int tipoServicio, bool tomaServicio, bool yaEligio)
        {
            ClienteTemporal clienteTemporal = new ClienteTemporal(estado, inicioAtencion, id, tipoServicio, tomaServicio, yaEligio);

            //Aprovechamos y la agregamos a la lista de objetos temporales
            fila1.estadoClientes.Add(clienteTemporal);

            return clienteTemporal;
         }

        //Este metodo se hace para calcular el tiempo de espera y porcentaje de cada objeto temporal
        //Se activa cada vez que un objeto egresa de la cola
        public void Cola(ClienteTemporal clienteTemporal)
        {   
            //En caso de que el objeto nunca haya formado aprte de la cola, entonces el resultado será 0
            fila1.cola[i].tiempoEspera += fila1.reloj - clienteTemporal.inicioAtencion;
            fila1.cola[i].PRCtiempoFuera = (fila1.cola[clienteTemporal.tipoServicio].tiempoEspera / fila1.reloj) * 100;
        }

        //Genera el fin, se le ingresa el tipo de fin y el clienteTemporal al que esta asignado
        public void GenerarFin(int i, ClienteTemporal clienteTemporal)
        {

            //Va a buscar si uno de los servicios esta desocupado
            for(int j = 0; j < fila1.estados[i].Count; j++)
            {
                if (fila1.estados[i][j] == "Libre")
                {
                    fila1.estados[i][j] = "Ocupado";
                    clienteTemporal.estado = "Atendiendo";
                    clienteTemporal.inicioAtencion = fila1.reloj;

                    fila1.fin[i].clienteTemporal[j] = clienteTemporal;

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

                    Cola(clienteTemporal);
                    return;
                }
            }

            //Si ninguno de los objetos permanentes esta disponible, entonces agrega al objeto temporal a la cola que le corresponde
            fila1.cola[i].cantidad.Add(clienteTemporal);
        }

        //Es el comienzo del fin, se le envia el tipo de fin y el numero del servidor (se puede sacar de la posicion
        //en la que se encontraba el tiempo del evento)
        public void ComienzaFin(int tipoFin, int servidorFin)
        {

            /***********************************************************************************
             
             Aqui es donde se revisaria si el cliente quiere revisar el servicio adicional o no

             ************************************************************************************/
            ClienteTemporal clienteTemporal = fila1.fin[tipoFin].clienteTemporal[servidorFin];

            if(clienteTemporal.yaEligio = false)
            {
                FinEspecial(ClienteTemporal clienteTemporal);
            }

            if (clienteTemporal.tomaServicio == false)
            {
                //Va a buscar entre los clientes temporales aquel que pertenezca al fin que comienza
                fila1.estadoClientes.Remove(fila1.fin[i].clienteTemporal[servidorFin]);


                //Revisa si hay algun objeto temporal en la cola, para comenzar con su procesamiento
                if (fila1.cola[i].cantidad.Count != 0)
                {
                    //Revisa todos los clientes temporales
                    foreach (var cliente in fila1.estadoClientes)
                    {

                        //Busca aquel cuyo id sea igual al del primero de la cola (el primero que entró)
                        if (cliente.id == fila1.cola[tipoFin].cantidad[0].id)
                        {
                            //Cambia el estado del objeto temporal de "En espera" a "Atendido"
                            //RECORDAR, EL OBJETO YA EXISTIA, SOLO ESTABA "En espera"
                            cliente.estado = "Atendido";
                            cliente.inicioAtencion = fila1.reloj;

                            ClienteTemporal clienteCola = fila1.cola[tipoFin].cantidad[0];

                            //Genero el nuevo fin del mismo tipo y con el ClienteTemporal de la cola
                            GenerarFin(int i, clienteCola);

                            fila1.cola[tipoFin].cantidad.RemoveAt(0);
                            return;
                        }
                    }
                }

                fila1.fin[tipoFin].finAtencion[servidorFin] = 0;
                fila1.estados[tipoFin][servidorFin] = "Libre";



            }
        }

    }

    



}
