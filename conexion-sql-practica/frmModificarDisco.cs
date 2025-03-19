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
    public partial class frmModificarDisco : Form
    {
        private Disco disco;
        public frmModificarDisco()
        {
            InitializeComponent();
        }

        public frmModificarDisco(Disco disco)
        {
            InitializeComponent();
            this.disco = disco;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Se crea una nueva instancia de Disco y de la capa de negocio
            
            DiscoNegocio discoNegocio = new DiscoNegocio();

            try
            {
                // Se asignan los valores ingresados en el formulario al objeto discoNuevo
                
                disco.Nombre = txbTituloMod.Text;
                disco.FechaLanzamiento = dtpFechaMod.Value; // Se obtiene la fecha del control DateTimePicker
                disco.CantidadCanciones = int.Parse(txbCantidadCancionesMod.Text); // Se convierte el texto en un número entero
                disco.UrlImagenTapa = txbUrlTapaMod.Text;
                disco.generoDisco = (Genero)cbxGeneroMod.SelectedItem; // Se asigna el género seleccionado en el ComboBox
                disco.formatoDisco = (TipoEdicion)cbxFormatoMod.SelectedItem; // Se asigna el formato seleccionado en el ComboBox

                // Se llama a la capa de negocio para agregar el nuevo disco
                discoNegocio.modificar(disco);

                // Se muestra un mensaje de éxito
                MessageBox.Show("Disco modificado exitosamente");

                // Se cierra el formulario
                Close();
            }
            catch (Exception err)
            {
                // En caso de error, se muestra un mensaje con la descripción de la excepción
                MessageBox.Show(err.ToString());
            }
        }

        private void frmModificarDisco_Load(object sender, EventArgs e)
        {
            GeneroNegocio generoNegocio = new GeneroNegocio();
            EdicionNegocio edicionNegocio = new EdicionNegocio();

            try
            {
                // Se llenan los ComboBox con los datos obtenidos de la capa de negocio
                cbxGeneroMod.DataSource = generoNegocio.listar();
                cbxGeneroMod.ValueMember = "id";
                cbxGeneroMod.DisplayMember = "descripcion";
                cbxFormatoMod.DataSource = edicionNegocio.listar();
                cbxFormatoMod.ValueMember = "id";
                cbxFormatoMod.DisplayMember = "formato";
            }
            catch (Exception err)
            {
                // En caso de error, se muestra un mensaje con la descripción de la excepción
                MessageBox.Show(err.ToString());
            }

            cargarImagen(disco.UrlImagenTapa);
            txbTituloMod.Text = disco.Nombre;
            dtpFechaMod.Value = disco.FechaLanzamiento;
            txbCantidadCancionesMod.Text = disco.CantidadCanciones.ToString();
            txbUrlTapaMod.Text = disco.UrlImagenTapa;
            cbxGeneroMod.SelectedValue = disco.generoDisco.id;
            cbxFormatoMod.SelectedValue = disco.formatoDisco.id;

        }

        

        private void cargarImagen(string img)
        {

            // funcion que recibe como parametro la url de una imagen

            try
            {
                pbxDiscosMod.Load(img);  // Load muestra la imagen de la url en el picture box
            }
            catch (Exception)
            {
                pbxDiscosMod.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }

        private void txbUrlTapaMod_Leave(object sender, EventArgs e)
        {
            cargarImagen(txbUrlTapaMod.Text);
        }
    }
}
