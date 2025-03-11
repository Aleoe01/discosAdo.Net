using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;


namespace negocio
{
    public class DiscoNegocio
    {

        // Metodo para traer todos los discos de la BD y guardarlos en una lista
        public List<Disco> listar()
        {

            // Lista donde vamos a ir apilando los discos leidos de la BD
            List<Disco> lista = new List<Disco>();

            // AccesoDatos es la clase encargada de conectarse a la BD, por eso creamos una instancia para usar sus metodos
            AccesoDatos datos = new AccesoDatos();

            try
            {

                // definimos nuestra consulta y la pasamos como parametro
                datos.setearConsulta("select d.Titulo, d.FechaLanzamiento, d.CantidadCanciones, d.UrlImagenTapa, e.Descripcion as estilo, t.Descripcion as formato from DISCOS d, ESTILOS e, TIPOSEDICION t where d.IdEstilo = e.Id and d.IdTipoEdicion = t.Id");
                // se ejecuta la consulta y se guardan los datos obtenidos en el lector
                datos.ejecutarLectura();

                // recorremos el lector y vamos leyendo los datos uno por uno en cada vuelta del while
                while (datos.Lector.Read()) 
                { 
                    Disco aux = new Disco(); 
                    aux.Nombre = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
                    aux.UrlImagenTapa = (string)datos.Lector["UrlImagenTapa"];
                    aux.generoDisco = new Genero();
                    aux.generoDisco.descripcion = (string)datos.Lector["estilo"];
                    aux.formatoDisco = new TipoEdicion();
                    aux.formatoDisco.formato = (string)datos.Lector["formato"];   

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception err)
            {
                throw err;
            }

            finally 
            {
                datos.cerrarLectura();
            }
            
            
        }
        public void agregar(Disco nuevo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("insert into DISCOS values ('" + nuevo.Nombre + "','"+ nuevo.FechaLanzamiento +"'," + nuevo.CantidadCanciones + ",'" + nuevo.UrlImagenTapa + "',"+ nuevo.generoDisco.id +","+ nuevo.formatoDisco.id +")");
                accesoDatos.ejecutarNonQuerry();
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                accesoDatos.cerrarLectura();
            }
        }
    }
}
