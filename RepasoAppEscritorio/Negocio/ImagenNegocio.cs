using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> Listar()
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES");
                datos.EjecutarLectura();

                while(datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    imagenes.Add(aux);
                }

                return imagenes;
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

        public void Agregar(Imagen aux)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
            datos.SetearConsulta("INSERT INTO IMAGENES(IdArticulo, ImagenUrl)\r\nVALUES(@IdArticulo, @ImagenUrl)");
            datos.SetearParametro("@IdArticulo", aux.IdArticulo);
            datos.SetearParametro("@ImagenUrl", aux.ImagenUrl);
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

        public void Modificar(Imagen aux)
        {

        }

        public void CargarListas(List<Articulo> aux)
        {
            List<Imagen> lista = Listar();


            try
            {
            foreach (Articulo articulo in aux)
            {
                foreach(Imagen img in lista)
                {
                    if(articulo.Id == img.IdArticulo)
                    {
                        articulo.Imagenes.Add(img);
                    }
                }
            }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CargarLista(Articulo aux)
        {
            List<Imagen> lista = Listar();

            try
            {
                foreach (Imagen img in lista)
                {
                    if(img.IdArticulo == aux.Id)
                    {
                        aux.Imagenes.Add(img);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
