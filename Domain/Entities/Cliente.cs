using Domain.Common;

namespace Domain.Entities
{
    public class Cliente : AudutableBaseEntity
    {
        public string Nombre { get; set; }

        public string Telefono { get; set; }
    }
}
