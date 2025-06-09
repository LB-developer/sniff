using System.Collections.Generic;
using Commands;
using Git;
using NUnit.Framework;

public static class TestData
{
    public static IEnumerable<TestCaseData> GitParseCases()
    {
        yield return new TestCaseData(
                new Result { stdOut = GitStatusMocks.CleanRepo(), stdErr = "", exitCode = 0 },
                new Status
                {
                    branch = "main",
                    hasUncommittedChanges = false,
                    hasUntrackedFiles = false,
                    isAheadOfRemote = false,
                    isBehindRemote = false,
                    isGitRepo = true,
                }).SetName("CleanRepo");

        yield return new TestCaseData(
                new Result { stdOut = GitStatusMocks.ModifiedFiles(), stdErr = "", exitCode = 0 },
                new Status
                {
                    branch = "main",
                    hasUncommittedChanges = true,
                    hasUntrackedFiles = false,
                    isAheadOfRemote = false,
                    isBehindRemote = false,
                    isGitRepo = true,
                }).SetName("ModifiedFiles");


        yield return new TestCaseData(
                new Result { stdOut = GitStatusMocks.StagedAndUntracked(), stdErr = "", exitCode = 0 },
                new Status
                {
                    branch = "main",
                    hasUncommittedChanges = true,
                    hasUntrackedFiles = true,
                    isAheadOfRemote = false,
                    isBehindRemote = false,
                    isGitRepo = true,
                }).SetName("StageAndUntracked");

        yield return new TestCaseData(
                new Result { stdOut = GitStatusMocks.ModifiedAndStagedAndUntracked(), stdErr = "", exitCode = 0 },
                new Status
                {
                    branch = "main",
                    hasUncommittedChanges = true,
                    hasUntrackedFiles = true,
                    isAheadOfRemote = false,
                    isBehindRemote = false,
                    isGitRepo = true,
                }).SetName("ModifiedAndStagedAndUntracked");

        yield return new TestCaseData(
                new Result { stdOut = "", stdErr = GitStatusMocks.NotARepo(), exitCode = 0 },
                new Status
                {
                    branch = "",
                    hasUncommittedChanges = false,
                    hasUntrackedFiles = false,
                    isAheadOfRemote = false,
                    isBehindRemote = false,
                    isGitRepo = false,
                }).SetName("NotARepo");
    }
}


public static class GitStatusMocks
{
    public static string CleanRepo(string branch = "main") =>
        string.Join('\n', new[]
        {
            $"## {branch}...origin/{branch}"
        });

    public static string ModifiedFiles(string branch = "main") =>
        string.Join('\n', new[]
        {
            $"## {branch}...origin/{branch}",
            " M Program.cs"
        });

    public static string StagedAndUntracked(string branch = "main") =>
        string.Join('\n', new[]
        {
            $"## {branch}...origin/{branch}",
            "A  added.cs",
            "?? newfile.txt"
        });

    public static string ModifiedAndStagedAndUntracked(string branch = "main") =>
        string.Join('\n', new[]
        {
            $"## {branch}...origin/{branch}",
            " M modified.cs",
            "A staged.cs",
            "D deleted.cs",
            "?? untracked.cs"
        });

    public static string NotARepo(string branch = "main") =>
        "fatal: not a git repository (or any of the parent directories): .git";
}
