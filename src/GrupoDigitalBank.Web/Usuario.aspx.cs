using System;
using System.Linq;
using System.ServiceModel;
using GrupoDigitalBank.Contratos;
using GrupoDigitalBank.Dominio;
using UsuarioEntidad = GrupoDigitalBank.Dominio.Usuario;

namespace GrupoDigitalBank.Web
{
    public partial class Usuario : System.Web.UI.Page
    {
        private int? UsuarioId
        {
            get
            {
                int id;
                return int.TryParse(Request.QueryString["id"], out id) && id > 0 ? id : (int?)null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSexos();
                CargarUsuarioParaEdicion();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            DateTime fechaNacimiento;

            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
            {
                MostrarMensaje("La fecha de nacimiento no tiene un formato valido.", esExitoso: false);
                return;
            }

            UsuarioEntidad usuario = new UsuarioEntidad
            {
                Id = UsuarioId.GetValueOrDefault(),
                Nombre = txtNombre.Text.Trim(),
                FechaNacimiento = fechaNacimiento,
                Sexo = ddlSexo.SelectedValue
            };

            try
            {
                ResultadoOperacion resultado = EjecutarServicio(servicio =>
                    UsuarioId.HasValue ? servicio.Modificar(usuario) : servicio.Agregar(usuario));

                MostrarMensaje(resultado.Mensaje, resultado.Exitoso);

                if (resultado.Exitoso && !UsuarioId.HasValue)
                {
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("No fue posible guardar el usuario. " + ex.Message, esExitoso: false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UsuarioConsulta.aspx", endResponse: true);
        }

        private void CargarSexos()
        {
            ddlSexo.Items.Clear();
            ddlSexo.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            ddlSexo.Items.Add(new System.Web.UI.WebControls.ListItem("Masculino", "M"));
            ddlSexo.Items.Add(new System.Web.UI.WebControls.ListItem("Femenino", "F"));
        }

        private void CargarUsuarioParaEdicion()
        {
            if (!UsuarioId.HasValue)
            {
                return;
            }

            try
            {
                UsuarioEntidad usuario = EjecutarServicio(servicio =>
                    servicio.Consultar().FirstOrDefault(item => item.Id == UsuarioId.Value));

                if (usuario == null)
                {
                    MostrarMensaje("No se encontro el usuario solicitado para modificar.", esExitoso: false);
                    return;
                }

                txtNombre.Text = usuario.Nombre;
                txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString("yyyy-MM-dd");
                ddlSexo.SelectedValue = usuario.Sexo;
                btnGuardar.Text = "Modificar";
            }
            catch (Exception ex)
            {
                MostrarMensaje("No fue posible cargar el usuario. " + ex.Message, esExitoso: false);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            ddlSexo.SelectedValue = string.Empty;
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
