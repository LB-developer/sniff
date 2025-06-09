namespace Commands;

public class Result
{
    public string stdOut { get; set; } = "";
    public string stdErr { get; set; } = "";
    public int exitCode { get; set; }

    public bool isSuccess => exitCode == 0;
    public bool hasFatalError => stdErr.Contains("fatal:");
}
