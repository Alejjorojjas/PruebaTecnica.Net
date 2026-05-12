using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using GrupoDigitalBank.Contratos;
using GrupoDigitalBank.Dominio;

namespace GrupoDigitalBank.ServicioWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly UsuarioRepositorio repositorio = new UsuarioRepositorio();

        public ResultadoOperacion Agregar(Usuario usuario)
        {
            try
            {
                ValidarUsuario(usuario, requiereId: false);
                return repositorio.Agregar(usuario);
            }
            catch (Exception ex)
            {
                return CrearResultadoFallido(ex);
            }
        }

        public ResultadoOperacion Modificar(Usuario usuario)
        {
            try
            {
                ValidarUsuario(usuario, requiereId: true);
                return repositorio.Modificar(usuario);
            }
            catch (Exception ex)
            {
                return CrearResultadoFallido(ex);
            }
        }

        public List<Usuario> Consultar()
        {
            try
            {
                return repositorio.Consultar();
            }
            catch (Exception ex)
            {
                throw new FaultException("No fue posible consultar los usuarios. " + ex.Message);
            }
        }

        public ResultadoOperacion Eliminar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Debe indicar un usuario valido para eliminar.");
                }

                return repositorio.Eliminar(id);
            }
            catch (Exception ex)
            {
                return CrearResultadoFallido(ex);
            }
        }

        private static void ValidarUsuario(Usuario usuario, bool requiereId)
        {
            if (usuario == null)
            {
                throw new ArgumentException("La informacion del usuario es obligatoria.");
            }

            if (requiereId && usuario.Id <= 0)
            {
                throw new ArgumentException("Debe indicar un usuario valido para modificar.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (usuario.Nombre.Trim().Length > 100)
            {
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.");
            }

            if (usuario.FechaNacimiento.Date > DateTime.Today)
            {
                throw new ArgumentException("La fecha de nacimiento no puede ser futura.");
            }

            if (usuario.Sexo != "M" && usuario.Sexo != "F")
            {
                throw new ArgumentException("El sexo debe ser M o F.");
            }
        }

        private static ResultadoOperacion CrearResultadoFallido(Exception ex)
        {
            return new ResultadoOperacion
            {
                Exitoso = false,
                Mensaje = ex.Message
            };
        }
    }
}
