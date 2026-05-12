using System;
using System.Collections.Generic;
using System.ServiceModel;
using GrupoDigitalBank.Contratos;
using UsuarioEntidad = GrupoDigitalBank.Dominio.Usuario;

namespace GrupoDigitalBank.Web
{
    public partial class UsuarioConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        protected void gvUsuarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName != "EliminarUsuario")
            {
                return;
            }

            int id;

            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id))
            {
                MostrarMensaje("No fue posible identificar el usuario a eliminar.", esExitoso: false);
                return;
            }

            try
            {
                var resultado = EjecutarServicio(servicio => servicio.Eliminar(id));
                MostrarMensaje(resultado.Mensaje, resultado.Exitoso);
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MostrarMensaje("No fue posible eliminar el usuario. " + ex.Message, esExitoso: false);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                List<UsuarioEntidad> usuarios = EjecutarServicio(servicio => servicio.Consultar());
                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("No fue posible consultar los usuarios. " + ex.Message, esExitoso: false);
            }
        }

        private void MostrarMensaje(string mensaje, bool esExitoso)
        {
            pnlMensaje.Visible = true;
            pnlMensaje.CssClass = esExitoso ? "alert alert-ok" : "alert alert-error";
            litMensaje.Text = Server.HtmlEncode(mensaje);
        }

        private static T EjecutarServicio<T>(Func<IUsuarioServicio, T> accion)
        {
            ChannelFactory<IUsuarioServicio> factory = new ChannelFactory<IUsuarioServicio>("UsuarioServicioEndpoint");
            IUsuarioServicio proxy = factory.CreateChannel();
            IClientChannel canal = (IClientChannel)proxy;

            try
            {
                T resultado = accion(proxy);
                canal.Close();
                factory.Close();
                return resultado;
            }
            catch
            {
                canal.Abort();
                factory.Abort();
                throw;
            }
        }
    }
}
