namespace Git;

public class Status
{
    public string branch { get; set; } = "";
    public bool hasUncommittedChanges { get; set; }
    public bool hasUntrackedFiles { get; set; }
    public bool isAheadOfRemote { get; set; }
    public bool isBehindRemote { get; set; }
    public bool isGitRepo { get; set; }
}
