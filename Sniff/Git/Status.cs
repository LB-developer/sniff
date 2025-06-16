namespace Git;

public class Status
{
    public string branch { get; set; } = "";
    public int uncommittedChanges { get; set; }
    public int addedFiles { get; set; }
    public int deletedFiles { get; set; }
    public int modifiedFiles { get; set; }
    public bool isAheadOfRemote { get; set; }
    public bool isBehindRemote { get; set; }
    public bool isGitRepo { get; set; }
}
