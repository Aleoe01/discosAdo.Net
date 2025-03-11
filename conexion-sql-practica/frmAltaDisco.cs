using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace conexion_sql_practica
{
    public partial class frmAltaDisco : Form
    {
        public frmAltaDisco()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se hace clic en el botón "Aceptar"
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Se crea una nueva instancia de Disco y de la capa de negocio
            Disco discoNuevo = new Disco();
            DiscoNegocio discoNegocio = new DiscoNegocio();

            try
            {
                // Se asignan los valores ingresados en el formulario al objeto discoNuevo
                discoNuevo.Nombre = txbTitulo.Text;
                discoNuevo.FechaLanzamiento = dtpFecha.Value; // Se obtiene la fecha del control DateTimePicker
                discoNuevo.CantidadCanciones = int.Parse(txbCantidadCanciones.Text); // Se convierte el texto en un número entero
                discoNuevo.UrlImagenTapa = txbUrlTapa.Text;
                discoNuevo.generoDisco = (Genero)cbxGenero.SelectedItem; // Se asigna el género seleccionado en el ComboBox
                discoNuevo.formatoDisco = (TipoEdicion)cbxFormato.SelectedItem; // Se asigna el formato seleccionado en el ComboBox

                // Se llama a la capa de negocio para agregar el nuevo disco
                discoNegocio.agregar(discoNuevo);

                // Se muestra un mensaje de éxito
                MessageBox.Show("Disco agregado exitosamente");

                // Se cierra el formulario
                Close();
            }
            catch (Exception err)
            {
                // En caso de error, se muestra un mensaje con la descripción de la excepción
                MessageBox.Show(err.ToString());
            }
        }

        // Evento que se ejecuta cuando se hace clic en el botón "Cancelar"
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Se cierra el formulario sin realizar ninguna acción
            this.Close();
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void frmAltaDisco_Load(object sender, EventArgs e)
        {
            // Se crean instancias de las capas de negocio para género y edición
            GeneroNegocio generoNegocio = new GeneroNegocio();
            EdicionNegocio edicionNegocio = new EdicionNegocio();

            try
            {
                // Se llenan los ComboBox con los datos obtenidos de la capa de negocio
                cbxGenero.DataSource = generoNegocio.listar();
                cbxFormato.DataSource = edicionNegocio.listar();
            }
            catch (Exception err)
            {
                // En caso de error, se muestra un mensaje con la descripción de la excepción
                MessageBox.Show(err.ToString());
            }
        }

        private void txbUrlTapa_Leave(object sender, EventArgs e)
        {
            cargarImagen(txbUrlTapa.Text);

        }

        private void cargarImagen(string img)
        {

            // funcion que recibe como parametro la url de una imagen

            try
            {
                pbxDiscos.Load(img);  // Load muestra la imagen de la url en el picture box
            }
            catch (Exception)
            {
                pbxDiscos.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }
    }
}
