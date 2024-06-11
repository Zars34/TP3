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

        public void Llegada(int i)
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
                        Cola(clienteTemporal, i);
                        break;
                    }
                    else
                    {
                        //Si no hay ninguno libre, tiene que esperar
                        ClienteTemporal clienteTemporal = SetObjetoTemporal("Esperando", 0.0);

                        fila1.cola[i].cantidad.Add(clienteTemporal);
                    
                    }

                }
            
        }

        //Revisa si al comienzo el cliente quiere realizar el servicio especial o no
        public void LlegadaEspecial()
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
            
        }
        public void LlegadaServicioEspecial()
        {
            /*
             Colocar calculo de cada llegadaEspecial
             */

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
        }

        //Revisa si al final el cliente quiere realizar el servicio especial o no
        public void FinEspecial()
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

                /*
                 Hay que determinar como enviar la columna a la que queremos que se asigne el proximo
                tiempo de fin
                 */

                //FinLlegada(5, nroColumna??);
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


        //Aqui es donde se realiza el Fin de una llegada; solo tenemos que indicar a que llegada
        //ser refiere (lo que hace en cuanto llega una llamada como evento) y el numero de la cola
        //al que esté asignada la llamada
        public void FinLlegada(int i, int nroCola)
        {

            Random random = new Random();

            // Generar un número decimal aleatorio entre 0.01 y 0.99
            double numeroDecimalAleatorio = random.NextDouble() * (0.99 - 0.01) + 0.01;

            // Redondear a dos decimales
            numeroDecimalAleatorio = Math.Round(numeroDecimalAleatorio, 2);

            fila1.fin[i].RND = numeroDecimalAleatorio;
            fila1.fin[i].tiempo = -fila1.fin[i].media * Math.Log(1 - numeroDecimalAleatorio);
            fila1.fin[i].ACTiempoAtencion += fila1.fin[i].tiempo;
            fila1.fin[i].PRCOcupacion = fila1.fin[i].ACTiempoAtencion / fila1.reloj;


            if (fila1.fin[i].finAtencion[nroCola - 1] = null)
            {
                fila1.fin[i].finAtencion[nroCola - 1] = fila1.reloj + fila1.fin[i].finAtencion[nroCola - 1].tiempo;
            }

        }



    }

        

    
}
