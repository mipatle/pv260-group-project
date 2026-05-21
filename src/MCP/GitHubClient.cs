namespace MCP;

using System.Net.Http.Headers;

public class GitHubClient
{
    private readonly HttpClient _client;
    private readonly string _repo;

    public GitHubClient()
    {
        _repo = Environment.GetEnvironmentVariable("GITHUB_REPO") ?? "";
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

        if (!response.IsSuccessStatusCode)
            return $"GitHub API error: {(int)response.StatusCode} - {body}";

        return body;
    }

    public string Repo => _repo;
}