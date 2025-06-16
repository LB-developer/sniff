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
        Assert.That(actual.addedFiles == expected.addedFiles);
        Assert.That(actual.uncommittedChanges == expected.uncommittedChanges);
        Assert.That(actual.deletedFiles == expected.deletedFiles);
        Assert.That(actual.modifiedFiles == expected.modifiedFiles);
        Assert.That(actual.isAheadOfRemote == expected.isAheadOfRemote);
        Assert.That(actual.isBehindRemote == expected.isBehindRemote);
    }
}
