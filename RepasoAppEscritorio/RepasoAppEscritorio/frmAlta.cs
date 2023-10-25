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
        private Articulo articulo = null;

        public frmAlta()
        {
            InitializeComponent();
        }

        public frmAlta(Articulo seleccionado)
        {
            InitializeComponent();
            this.articulo = seleccionado;
            Text = "Modificar artículo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            if(articulo==null)
                articulo = new Articulo();

            articulo.Imagenes = new List<Imagen>();
            articulo.Imagen = new Imagen();
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descirpcion = txtDescripcion.Text;
                articulo.Imagen.ImagenUrl = txtImagen.Text;
                imagenNegocio.CargarLista(articulo);
                //LOS VALORES DE LOS COMBOBOX SE CASTEAN
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if(articulo.Id == 0)
                {
                negocio.Agregar(articulo);

                articulo.Imagen.IdArticulo = negocio.CapturarId(txtCodigo.Text);
                articulo.Imagen.ImagenUrl = txtImagen.Text;
                articulo.Imagenes.Add(articulo.Imagen);

                imagenNegocio.Agregar(articulo.Imagen);
                MessageBox.Show("Artículo agregado");
                }
                else
                {
                    negocio.Modificar(articulo);

                   
                    //imagenNegocio.Modificar(articulo.Imagen);
                    MessageBox.Show("Artículo modificado");
                }

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
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcaNegocio.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descirpcion;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtImagen.Text = articulo.Imagen.ImagenUrl;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cargarImagen(txtImagen.Text);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxAlta.Load(imagen);
            }
            catch (Exception)
            {

                pbxAlta.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTaqh0REQrSROCBa9Q9u79-LWiM8TRPAUCV4w&usqp=CAU");
            }
        }
    }
}
