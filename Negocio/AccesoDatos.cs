using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // Librería necesaria para la conexión a SQL Server

namespace negocio
{


    /*
     * La clase AccesoDatos se encarga de manejar la conexión con la base de datos SQL Server,
     * ejecutar consultas y recuperar información. Su propósito principal es simplificar el acceso
     * a la base de datos y separar la lógica de acceso a datos del resto de la aplicación.

     * Principales Funcionalidades:
     * ✅ Abrir y cerrar la conexión con la base de datos.
     * ✅ Ejecutar consultas SQL y recuperar resultados con SqlDataReader.
     * ✅ Centralizar el manejo de la base de datos en una única clase para facilitar el mantenimiento.

     * Esta clase suele utilizarse en una aplicación de capas (por ejemplo, en una capa de acceso a datos), 
     * permitiendo que otras partes del programa no tengan que preocuparse por la conexión a la base de datos 
     * ni por la ejecución de consultas.
     */


    public class AccesoDatos
    {
        // Variables privadas para manejar la conexión y la consulta
        private SqlConnection conexion; // Representa la conexión con la base de datos
        private SqlCommand comando;     // Ejecuta consultas SQL
        private SqlDataReader lector;   // Permite leer los resultados de la consulta

        // Propiedad pública para acceder al lector desde fuera de la clase
        public SqlDataReader Lector
        { 
            get { return lector; } 
        }
        
        // Constructor: cada vez que se crea una instancia de AccesoDatos se inicializa la conexion y el comando
        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true");
            comando = new SqlCommand();
        }

        // metodo para asignar nuestra consulta SQL al comando
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text; // se indica que la consulta sql es de tipo texto
            comando.CommandText = consulta;                     // carga nuestra consulta traida por paramentro al comando
        }
        
        // metodo para ejecutar la lectura y leer los datos traidos de la BD
        public void ejecutarLectura()
        {
            comando.Connection = conexion;          // se asigna al comando la conexion con nuestra BD

            try
            {
                conexion.Open();                    // abrimos la conexion        
                lector = comando.ExecuteReader();   // ejecutamos la consulta sobre la BD y los datos obtenidos se guardan en lector
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ejecutarNonQuerry() 
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        // metodo para cerrar la conexion
        public void cerrarLectura()
        {
            if (lector != null) // sí el lector fue inicializado se cierra
                lector.Close();
            conexion.Close();   // se cierra la conexion
        }

        



    }
}
