namespace BaseSource.Data.Entities
{
    public class PetProject
    {
        public int Id { get; set; }
        public int CategoryProjectId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string LinkDemo { get; set; }
        public string LinkSourceCode { get; set; }
        public string Image { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool Published { get; set; }
        public CategoryProject CategoryProject { get; set; }
    }
}
