using System.Diagnostics;

namespace Commands;

public static class Runner
{
    public static Result Run(string command, string args)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        string stdout;
        string stderr;
        int exitcode;

        using (var process = Process.Start(startInfo))
        {
            if (process is null)
            {
                Console.WriteLine("Couldn't start process, exiting...");
                Environment.Exit(1);
            }

            stdout = process.StandardOutput.ReadToEnd();
            stderr = process.StandardError.ReadToEnd();
            exitcode = process.ExitCode;
        }

        return new Result
        {
            stdOut = stdout.Trim(),
            stdErr = stderr.Trim(),
            exitCode = exitcode
        };

    }
}
