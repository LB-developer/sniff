using System.Text;
using Commands;

namespace Git;

public static class StatusParser
{
    public static Status Parse(Result result)
    {
        var status = new Status();

        if (result.stdErr.Contains("fatal: not a git repository"))
        {
            status.isGitRepo = false;
            return status;
        }

        status.isGitRepo = true;

        var lines = result.stdOut.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length > 0)
        {
            var firstLine = lines[0];
            status.branch = ParseBranch(firstLine);
            status.isAheadOfRemote = firstLine.Contains("ahead");
            status.isBehindRemote = firstLine.Contains("behind");
        }

        foreach (var line in lines.Skip(1))
        {
            if (line.StartsWith("??"))
                status.hasUntrackedFiles = true;
            else if (
                           line.StartsWith(" M")
                        || line.StartsWith("M ")
                        || line.StartsWith(" A")
                        || line.StartsWith("A ")
                        || line.StartsWith(" D")
                        || line.StartsWith("D ")
                    )
                status.hasUncommittedChanges = true;
        }

        return status;
    }

    private static string ParseBranch(string statusLine)
    {
        // Extract the branch name from something like: "## main...origin/main [ahead 1]"
        // which is produced by 'git status -sb'
        var parts = statusLine.Split(' ');
        if (parts.Length >= 2)
            return parts[1].Split("...")[0]; // ignore remote name
        return "unknown";
    }

    public static string ToMessage(Result result)
    {
        var parsed = Parse(result);

        if (!parsed.isGitRepo)
            return "📂 Not a git repository";

        var sb = new StringBuilder();
        sb.AppendLine($"Branch: {parsed.branch}");
        sb.AppendLine($"Uncommitted changes?: {parsed.hasUncommittedChanges}");
        sb.AppendLine($"Untracked files?: {parsed.hasUntrackedFiles}");

        if (parsed.isAheadOfRemote)
            sb.AppendLine("Ahead of remote: True");
        else if (parsed.isBehindRemote)
            sb.AppendLine("Behind remote: True");
        else
            sb.AppendLine("Branch is in sync with remote");

        return sb.ToString();
    }
}


