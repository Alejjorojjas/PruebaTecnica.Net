using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GrupoDigitalBank.Dominio;

namespace GrupoDigitalBank.ServicioWCF
{
    internal class UsuarioRepositorio
    {
        private readonly string cadenaConexion;

        public UsuarioRepositorio()
        {
            ConnectionStringSettings configuracion = ConfigurationManager.ConnectionStrings["GrupoDigitalBankDb"];

            if (configuracion == null || string.IsNullOrWhiteSpace(configuracion.ConnectionString))
            {
                throw new ConfigurationErrorsException("No se encontro la cadena de conexion GrupoDigitalBankDb.");
            }

            cadenaConexion = configuracion.ConnectionString;
        }

        public ResultadoOperacion Agregar(Usuario usuario)
        {
            return EjecutarOperacionUsuario("AGREGAR", usuario);
        }

        public ResultadoOperacion Modificar(Usuario usuario)
        {
            return EjecutarOperacionUsuario("MODIFICAR", usuario);
        }

        public ResultadoOperacion Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = CrearComando(conexion, "ELIMINAR"))
            {
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return LeerResultadoOperacion(lector);
                }
            }
        }

        public List<Usuario> Consultar()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = CrearComando(conexion, "CONSULTAR"))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = Convert.ToInt32(lector["Id"]),
                            Nombre = Convert.ToString(lector["Nombre"]),
                            FechaNacimiento = Convert.ToDateTime(lector["FechaNacimiento"]),
                            Sexo = Convert.ToString(lector["Sexo"])
                        });
                    }
                }
            }

            return usuarios;
        }

        private ResultadoOperacion EjecutarOperacionUsuario(string operacion, Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = CrearComando(conexion, operacion))
            {
                if (usuario.Id > 0)
                {
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = usuario.Id;
                }

                comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = usuario.Nombre.Trim();
                comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
                comando.Parameters.Add("@Sexo", SqlDbType.Char, 1).Value = usuario.Sexo;

                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return LeerResultadoOperacion(lector);
                }
            }
        }

        private static SqlCommand CrearComando(SqlConnection conexion, string operacion)
        {
            SqlCommand comando = new SqlCommand("dbo.sp_Usuario_CRUD", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("@Operacion", SqlDbType.VarChar, 10).Value = operacion;
            return comando;
        }

        private static ResultadoOperacion LeerResultadoOperacion(SqlDataReader lector)
        {
            if (!lector.Read())
            {
                return new ResultadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "La base de datos no retorno resultado para la operacion."
                };
            }

            return new ResultadoOperacion
            {
                Exitoso = true,
                Mensaje = Convert.ToString(lector["Mensaje"]),
                IdGenerado = lector["Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["Id"])
            };
        }
    }
}
