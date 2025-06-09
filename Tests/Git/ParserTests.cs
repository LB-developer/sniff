using NUnit.Framework;
using Commands;
using Git;

namespace Tests;

[TestFixture]
public class GitParserTests
{

    [Test, TestCaseSource(typeof(TestData), nameof(TestData.GitParseCases))]
    public void ParseReturnsExpectedStatus(Result res, Status expected)
    {
        Status actual = StatusParser.Parse(res);

        Assert.That(actual.isGitRepo == expected.isGitRepo);
        Assert.That(actual.hasUncommittedChanges == expected.hasUncommittedChanges);
        Assert.That(actual.hasUntrackedFiles == expected.hasUntrackedFiles);
        Assert.That(actual.isAheadOfRemote == expected.isAheadOfRemote);
        Assert.That(actual.isBehindRemote == expected.isBehindRemote);
    }
}
