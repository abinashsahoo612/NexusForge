using TaskManagementSystem.Models.Workspaces;

public interface IWorkspaceMemberRepository
{
    Task<WorkspaceMember> AddAsync(WorkspaceMember member);
    Task UpdateAsync(WorkspaceMember member);
    Task<IEnumerable<WorkspaceMember>> GetMembersAsync(int workspaceId);
    Task<WorkspaceMember?> GetMemberAsync(int workspaceId, string userId);
    Task<bool> IsMemberAsync(int workspaceId, string userId);
}