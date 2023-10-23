using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RepasoAppEscritorio
{
    public partial class frmAlta : Form
    {
        public frmAlta()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo aux = new Articulo();
            Imagen imagen = new Imagen();
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                aux.Codigo = txtCodigo.Text;
                aux.Nombre = txtNombre.Text;
                aux.Descirpcion = txtDescripcion.Text;
                //LOS VALORES DE LOS COMBOBOX SE CASTEAN
                aux.Categoria = (Categoria)cboCategoria.SelectedItem;
                aux.Marca = (Marca)cboMarca.SelectedItem;
                aux.Precio = decimal.Parse(txtPrecio.Text);

                negocio.Agregar(aux);

                imagen.IdArticulo = negocio.CapturarId(txtCodigo.Text);
                imagen.ImagenUrl = txtImagen.Text;
                aux.Imagenes.Add(imagen);

                imagenNegocio.Agregar(imagen);
                MessageBox.Show("Articulo agregado");
                Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmAlta_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cboCategoria.DataSource = categoriaNegocio.Listar();
                cboMarca.DataSource = marcaNegocio.Listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
