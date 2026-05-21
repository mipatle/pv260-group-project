using System.Net.Http.Headers;

namespace MCP;

public class GitHubClient
{
    private readonly HttpClient _client;

    public string Repo { get; }

    public GitHubClient()
    {
        Repo = Environment.GetEnvironmentVariable("GITHUB_REPO") ?? "";
        var token = Environment.GetEnvironmentVariable("GITHUB_PAT") ?? "";

        _client = new HttpClient();

        _client.DefaultRequestHeaders.UserAgent.ParseAdd("mcp-server");
        _client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");

        if (!string.IsNullOrWhiteSpace(token))
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<string> Get(string url)
    {
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadAsStringAsync();

        return !response.IsSuccessStatusCode ? $"GitHub API error: {(int)response.StatusCode} - {body}" : body;
    }
}