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

        // evaluate static properties
        var lines = result.stdOut.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length > 0)
        {
            var firstLine = lines[0];
            status.branch = ParseBranch(firstLine);
            status.isAheadOfRemote = firstLine.Contains("ahead");
            status.isBehindRemote = firstLine.Contains("behind");
        }

        // count changes in the output
        foreach (var line in lines.Skip(1))
        {
            if (line.StartsWith("??"))
            {
                status.uncommittedChanges++;
            }
            else if (line.StartsWith(" M") || line.StartsWith("M "))
            {
                status.modifiedFiles++;
            }
            else if (line.StartsWith(" A") || line.StartsWith("A "))
            {
                status.addedFiles++;
            }
            else if (line.StartsWith(" D") || line.StartsWith("D "))
            {
                status.deletedFiles++;
            }
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
        int totalWidth = 40;
        sb.AppendLine();
        sb.AppendLine($"Current Branch {String.Format("|{0,5}", parsed.branch)}".PadLeft(totalWidth));
        sb.AppendLine($"Uncommitted changes {String.Format("|{0,5}", parsed.uncommittedChanges)}".PadLeft(totalWidth));
        sb.AppendLine($"Added files {String.Format("|{0,5}", parsed.addedFiles)}".PadLeft(totalWidth));
        sb.AppendLine($"Deleted files {String.Format("|{0,5}", parsed.deletedFiles)}".PadLeft(totalWidth));
        sb.AppendLine($"Modified files {String.Format("|{0,5}", parsed.modifiedFiles)}".PadLeft(totalWidth));
        sb.AppendLine();

        if (parsed.isAheadOfRemote)
            sb.AppendLine("Ahead of remote: True".PadLeft(totalWidth));
        else if (parsed.isBehindRemote)
            sb.AppendLine("Behind remote: True".PadLeft(totalWidth));
        else
            sb.AppendLine("Branch is in sync with remote".PadLeft(totalWidth));

        return sb.ToString();
    }
}


