if (args.Length == 0 || args[0] == "-h" || args[0] == "--help")
{
    Console.WriteLine("Usage: sniff [command]");
    Console.WriteLine("Commands:");
    Console.WriteLine("  git      Inspect git status");
    Console.WriteLine("  -h       Show help");
    return;
}

if (args[0] == "git")
{
    var git = new Git.Entry();
}
