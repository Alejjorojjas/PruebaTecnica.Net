using System.Runtime.Serialization;

namespace GrupoDigitalBank.Dominio
{
    [DataContract]
    public class ResultadoOperacion
    {
        [DataMember]
        public bool Exitoso { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int? IdGenerado { get; set; }
    }
}
