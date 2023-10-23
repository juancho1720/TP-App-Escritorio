using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace RepasoAppEscritorio
{
    public partial class frmArticulos : Form
    {
        //INSTANCIAMOS UNA LISTA PARA PODER REUTILIZARLA 
        private List<Articulo> listaArticulos;
        private int index = 0;

        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            listaArticulos = negocio.Listar();
            imagenNegocio.CargarListas(listaArticulos);
            //CON DATASOURCE LE PASAMOS LOS DATOS QUE VA A MOSTRAR
            dgvArticulos.DataSource = listaArticulos;


            //OCULTAMOS COLUMNAS QUE NO QUEREMOS QUE SALGAN EN LA GRILLA
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;

            
        }

        //MÉTODO PARA CAMBIAR LA IMAGEN AL SELECCIONAR OTRA FILA
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            //HACEMOS UN CASTEO PORQUE DATABOUND NOS DEVUELVE UN OBJECT PERO NO SABE DE QUE TIPO
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            //PASAMOS LA URL DEL ARTICULO SELECCIONADO AL EVENTO LOAD
            //cargarImagen(seleccionado.Imagen.ImagenUrl);
            cargarImagen(seleccionado.Imagenes[0].ImagenUrl);
        }

        //MÉTODO PARA VALIDAR QUE EL ARTÍCULO TENGA IMAGEN POR DEFECTO (SINO SE ROMPE TODO)
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception )
            {

                pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTaqh0REQrSROCBa9Q9u79-LWiM8TRPAUCV4w&usqp=CAU");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlta frmAlta = new frmAlta();
            frmAlta.ShowDialog();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;


            
            if (index < (seleccionado.Imagenes.Count)-1)
            {
                index++;
                cargarImagen(seleccionado.Imagenes[index].ImagenUrl);
            }
            else
            {
                cargarImagen(seleccionado.Imagenes[0].ImagenUrl);
                index = 0;
            }
            
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            if(index>0)
            {
                cargarImagen(seleccionado.Imagenes[index-1].ImagenUrl);
                index--;              
            }
            else
            {
                cargarImagen(seleccionado.Imagenes[(seleccionado.Imagenes.Count) - 1].ImagenUrl);
                index = (seleccionado.Imagenes.Count) - 1;
            }
        }

        
    }
}
