using Demkin.Blog.Enum;

namespace Demkin.Blog.DTO.Role
{
    public class RoleDetailDto
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public int SortNumber { get; set; }

        public Status Status { get; set; }
    }
}