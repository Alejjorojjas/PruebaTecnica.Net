using System.Collections.Generic;
using System.ServiceModel;
using GrupoDigitalBank.Dominio;

namespace GrupoDigitalBank.Contratos
{
    [ServiceContract]
    public interface IUsuarioServicio
    {
        [OperationContract]
        ResultadoOperacion Agregar(Usuario usuario);

        [OperationContract]
        ResultadoOperacion Modificar(Usuario usuario);

        [OperationContract]
        List<Usuario> Consultar();

        [OperationContract]
        ResultadoOperacion Eliminar(int id);
    }
}
