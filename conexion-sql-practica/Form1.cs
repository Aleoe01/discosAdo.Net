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
    public partial class Form1 : Form
    {
        private List<Disco> listaDiscos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Evento que se ejecuta cuando carga el formulario
            
            Cargar();
            
        }

        private void Cargar()
        {

            DiscoNegocio discoNegocio = new DiscoNegocio();             // se crea una instancia de DiscoNegocio para hacer uso de sus metodos
            try
            {
                listaDiscos = discoNegocio.listar();                    // el metodo listar devuelve una objeto del tipo list
                dgvDiscos.DataSource = listaDiscos;                     // dataSource es un metodo de dataGridView que toma un list y lo modela en una tabla
                dgvDiscos.Columns["id"].Visible = false;
                dgvDiscos.Columns["UrlImagenTapa"].Visible = false;     // ocultamos la columna
                cargarImagen(listaDiscos[0].UrlImagenTapa);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {

            // cada vez que se cambia la fila seleccionada se ejecuta esta funcion

            Disco seleccion = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            cargarImagen(seleccion.UrlImagenTapa); // se carga y muestra la imagen de la row seleccionada
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaDisco agregar = new frmAltaDisco();
            agregar.ShowDialog();
            Cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco modificable = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            frmModificarDisco modificarDisco = new frmModificarDisco(modificable);
            modificarDisco.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DiscoNegocio discoNegocio = new DiscoNegocio();
            Disco select;

            try
            {
                select = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("Está seguro de eliminar el disco "+ select.Nombre + "?", "ATENCIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (respuesta == DialogResult.Yes)
                    discoNegocio.eliminar(select.Id);

                Cargar();
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
