using Commands;

namespace Git;

public class Entry
{
    private string finalMessage { get; set; } = "";
    private readonly Dictionary<(string, string), Func<Result, string>> commandParsers = new()
    {
        { ("git", "status -sb"), StatusParser.ToMessage }
    };

    public Entry()
    {
        var instructions = new (string, string)[]
        {
            ("git", "status -sb"),
        };

        foreach (var (command, args) in instructions)
        {
            Sniff(command, args, commandParsers[(command, args)]);
        }
    }

    private void Sniff(string command, string args, Func<Result, string> parser)
    {
        var res = Commands.Runner.Run(command, args);

        if (res.hasFatalError)
        {
            ReturnFailure("Fatal error detected, exiting...\n");
        }

        finalMessage += parser(res);

        ReturnSuccess(finalMessage);
    }

    private void ReturnFailure(string message)
    {
        Console.WriteLine(message);
        Environment.Exit(1);
    }

    private void ReturnSuccess(string message)
    {
        Console.Write($"-- Sniff Report for {Environment.CurrentDirectory} ------------\n");
        Console.WriteLine(message);
        Environment.Exit(0);
    }

}

