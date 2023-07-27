
using BaseSource.Shared.Enums;

namespace BaseSource.Data.Entities
{
    public class VoteProject
    {
        public int Id { get; set; }
        public int PetProjectId { get; set; }
        public string IP { get; set; }
        public VoteStatus Status { get; set; }

        public PetProject PetProject { get; set; }
    }
}
