using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class GeneroNegocio
    {
        public List<Genero> listar()
        {
            List<Genero> lista = new List<Genero>();

            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("select id, Descripcion from ESTILOS");
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Genero aux = new Genero();
                    aux.descripcion = (string)accesoDatos.Lector["Descripcion"];
                    aux.id = (int)accesoDatos.Lector["id"];

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
                accesoDatos.cerrarLectura();
            }
        }

    }
}
