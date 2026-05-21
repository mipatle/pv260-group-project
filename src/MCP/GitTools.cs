using System.Diagnostics;
using ModelContextProtocol.Server;

namespace MCP;

[McpServerToolType]
public class GitTools
{
    [McpServerTool]
    public async Task<string> GetCommitDiff(string commitId)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = $"show {commitId}",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        var process = Process.Start(psi)!;
        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        return output;
    }
}