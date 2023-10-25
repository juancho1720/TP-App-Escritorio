using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;


namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                //ESTABLECEMOS CONEXIÓN          MOTOR DE BD                 BD
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                //INDICAMOS TIPO DE COMANDO QUE VAMOS A UTILIZAR
                comando.CommandType = System.Data.CommandType.Text;
                //LE PASAMOS LA CONSULTA SQL
                comando.CommandText = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion , M.Descripcion as Marca, C.Descripcion as Categoria, I.ImagenUrl, I.Id AS IdImagen, I.IdArticulo, A.Precio, A.IdCategoria, A.IdMarca FROM ARTICULOS A LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo";
                //EJECUTAMOS EL COMANDO QUE HICIMOS RECIEN
                comando.Connection = conexion;
                //ABRIMOS LA CONEXIÓN
                conexion.Open();
                //REALIZAMOS LA LECTURA
                lector = comando.ExecuteReader();

                //LEEMOS LO QUE TRAE EL LECTOR Y LO CARGAMOS EN LA LISTA
                while(lector.Read())
                {
                    //CARGAMOS EL OBJETO CON EL ELEMENTO DE LA LISTA QUE ESTÁ LEYENDO EN ESA ITERACIÓN
                    Articulo aux = new Articulo();
                    //SI HAY COMPOSICIÓN INSTANCIAR OBJETOS DE CADA CLASE
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    aux.Imagen = new Imagen();
                    aux.Imagenes = new List<Imagen>();
                    //          TIPO DE DATO | NOMBRE EN LA CONSULTA SQL
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    if (!(lector["Nombre"] is DBNull))
                        aux.Nombre = (string)lector["Nombre"];
                    aux.Descirpcion = (string)lector["Descripcion"];
                    //HACER EL OVERRIDE A TOSTRING
                    aux.Marca.Descripcion = (string)lector["Marca"];
                    //HACER EL OVERRIDE A TOSTRING
                    aux.Categoria.Descripcion = (string)lector["Categoria"];
                    //HACER EL OVERRIDE A TOSTRING
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.Imagen.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Imagen.IdArticulo = (int)lector["IdArticulo"];
                    aux.Imagen.Id = (int)lector["IdImagen"];
                    aux.Precio = (decimal)lector["Precio"];
                    aux.Categoria.Id = (int)lector["IdCategoria"];
                    aux.Marca.Id = (int)lector["IdMarca"];

                    //LO AÑADIMOS A LA LISTA
                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //SIEMPRE CERRAMOS LA CONEXIÓN
            finally 
            { 
                conexion.Close(); 
            }
        }

        public void Agregar(Articulo aux)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)\r\nVALUES(@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@Precio)");
                datos.SetearParametro("@Codigo", aux.Codigo);
                datos.SetearParametro("@Nombre", aux.Nombre);
                datos.SetearParametro("@Descripcion", aux.Descirpcion);
                datos.SetearParametro("@IdMarca", aux.Marca.Id);
                datos.SetearParametro("@IdCategoria", aux.Categoria.Id);
                datos.SetearParametro("@Precio", aux.Precio);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Articulo aux)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @idCategoria, Precio = @precio\r\nWHERE Id = @id");
                datos.SetearParametro("@codigo", aux.Codigo);
                datos.SetearParametro("@nombre", aux.Nombre);
                datos.SetearParametro("@desc", aux.Descirpcion);
                datos.SetearParametro("@idMarca", aux.Marca.Id);
                datos.SetearParametro("@idCategoria", aux.Categoria.Id);
                datos.SetearParametro("@precio", aux.Precio);
                datos.SetearParametro("@id", aux.Id);

                datos.EjecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE FROM ARTICULOS WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int CapturarId(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            int id = 0;
            try
            {
                datos.SetearConsulta("SELECT Id FROM ARTICULOS WHERE Codigo = @Codigo");
                datos.SetearParametro("@Codigo", codigo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    id = (int)datos.Lector["Id"];
                }

                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            
        }
    }
}
