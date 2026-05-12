using System;
using System.Runtime.Serialization;

namespace GrupoDigitalBank.Dominio
{
    [DataContract]
    public class Usuario
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public DateTime FechaNacimiento { get; set; }

        [DataMember]
        public string Sexo { get; set; }
    }
}
