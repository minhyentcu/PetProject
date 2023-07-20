using BaseSource.ViewModels.Common;

namespace BaseSource.ViewModels.Project
{
    public class CategoryRequestDto : PageQuery
    {
        public string Name { get; set; }
        public bool IsAll { get; set; }
    }
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedTime { get; set; }
    }
    public class CategoryUpdateDto : CategoryCreateDto
    {

    }
}
