using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EdicionNegocio
    {
        public List<TipoEdicion> listar()
        {
			List<TipoEdicion> lista = new List<TipoEdicion>();
			AccesoDatos accesoDatos = new AccesoDatos();
			
			try
			{
				accesoDatos.setearConsulta("select id, Descripcion from TIPOSEDICION");
				accesoDatos.ejecutarLectura();

				while (accesoDatos.Lector.Read())
				{
					TipoEdicion aux = new TipoEdicion();

					aux.formato = (string)accesoDatos.Lector["Descripcion"];
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
