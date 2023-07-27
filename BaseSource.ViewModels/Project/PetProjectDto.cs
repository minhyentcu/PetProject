using BaseSource.ViewModels.Common;
using Microsoft.AspNetCore.Http;

namespace BaseSource.ViewModels.Project
{
    public class PetProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryProjectId { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string LinkDemo { get; set; }
        public string LinkSourceCode { get; set; }
        public string Image { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Published { get; set; }
        public int TotalVote
        {
            get
            {
                return TotalVoteUp - TotalVoteDown;
            }
        }
        public int TotalVoteUp { get; set; } = 0;
         public int TotalVoteDown { get; set; } = 0;
        public bool VoteUp { get; set; } = false;
        public bool VoteDown { get; set; } = false;
    }
    public class PetProjectCreateDto
    {
        public string Name { get; set; }
        public int CategoryProjectId { get; set; }
        public string Description { get; set; }
        public string LinkDemo { get; set; }
        public string LinkSourceCode { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
    }

    public class PetProjectRequestDto : PageQuery
    {
        public string Name { get; set; }
    }
    public class ProjectClientRequestDto : PageQuery
    {
        public string Name { get; set; }
        public string IP { get; set; }
    }
    public class PetProjectUpdateDto : PetProjectCreateDto
    {
        public int Id { get; set; }
    }
    public class VoteProjectUpdateDto
    {
        public int ProjectId { get; set; }
        public string IP { get; set; }
        public bool IsUp { get; set; }
        public bool IsDown { get; set; }
    }
}
