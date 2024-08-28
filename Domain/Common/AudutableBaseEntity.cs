
namespace Domain.Common
{
    public abstract class AudutableBaseEntity
    {
        public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created {  get; set; }
        public string LastModifyBy { get; set; }
        public DateTime? LastModify { get; set; }
    }
}
