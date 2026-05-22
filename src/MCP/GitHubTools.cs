using ModelContextProtocol.Server;

namespace MCP;

[McpServerToolType]
public class GitHubTools
{
    private readonly GitHubClient _github = new();

    [McpServerTool]
    public Task<string> ListRecentIssues()
    {
        var url = $"https://api.github.com/repos/{_github.Repo}/issues";
        return _github.Get(url);
    }

    [McpServerTool]
    public Task<string> GetRepositoryInfo()
    {
        var url = $"https://api.github.com/repos/{_github.Repo}";
        return _github.Get(url);
    }

    [McpServerTool]
    public Task<string> GetPullRequests()
    {
        var url = $"https://api.github.com/repos/{_github.Repo}/pulls?state=open";
        return _github.Get(url);
    }
}