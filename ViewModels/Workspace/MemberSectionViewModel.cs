using TaskManagementSystem.DTOs.Account;
using TaskManagementSystem.DTOs.Workspaces;

namespace TaskManagementSystem.ViewModels.Workspace
{
    public class MemberSectionViewModel
    {
        public int MemberCount { get; set; }

        public List<WorkspaceMemberDto> Members { get; set; } = new();

        public List<UserListDto> Users { get; set; } = new();
    }
}