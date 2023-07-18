namespace BaseSource.Data.Entities
{
    public class CategoryProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool Published { get; set; }
        public ICollection<PetProject> PetProjects { get; set; }
    }
}
