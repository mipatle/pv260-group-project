using System.Diagnostics;
using ModelContextProtocol.Server;
using static System.Text.RegularExpressions.Regex;

namespace MCP;

[McpServerToolType]
public class GitTools
{
    [McpServerTool]
    public async Task<string> GetCommitDiff(string commitId)
    {
        if (!IsMatch(commitId, "^[a-fA-F0-9]{7,40}$")) return "Invalid commit hash format.";

        var psi = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = $"show {commitId}",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        try
        {
            using var process = Process.Start(psi)!;
            var output = await process.StandardOutput.ReadToEndAsync();

            await process.WaitForExitAsync();

            return process.ExitCode != 0 ? $"Git command failed with exit code {process.ExitCode}." : output;
        }
        catch (Exception ex)
        {
            return $"Error executing git show {commitId}.";
        }
    }
}