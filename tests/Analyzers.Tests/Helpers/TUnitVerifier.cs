using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;

namespace Analyzers.Tests.Helpers;

internal sealed class TUnitVerifier : IVerifier
{
    private readonly ImmutableStack<string> _context;

    public TUnitVerifier()
        : this(ImmutableStack<string>.Empty) { }

    private TUnitVerifier(ImmutableStack<string> context) => _context = context;

    public void Empty<T>(string collectionName, IEnumerable<T> collection)
    {
        if (collection.Any())
            Assert.Fail(CreateMessage($"'{collectionName}' is not empty"));
    }

    public void Equal<T>(T expected, T actual, string? message = null)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
            Assert.Fail(
                CreateMessage(
                    message ?? $"items not equal.  expected:'{expected}' actual:'{actual}'"
                )
            );
    }

    public void True([DoesNotReturnIf(false)] bool assert, string? message = null)
    {
        if (!assert)
            Assert.Fail(CreateMessage(message ?? "Expected value to be 'true' but was 'false'"));
    }

    public void False([DoesNotReturnIf(true)] bool assert, string? message = null)
    {
        if (assert)
            Assert.Fail(CreateMessage(message ?? "Expected value to be 'false' but was 'true'"));
    }

    [DoesNotReturn]
    public void Fail(string? message = null)
    {
        Assert.Fail(CreateMessage(message ?? "Verification failed for an unspecified reason."));
        throw new InvalidOperationException("Unreachable");
    }

    public void LanguageIsSupported(string language)
    {
        if (language != LanguageNames.CSharp && language != LanguageNames.VisualBasic)
            Assert.Fail(CreateMessage($"Unsupported Language: '{language}'"));
    }

    public void NotEmpty<T>(string collectionName, IEnumerable<T> collection)
    {
        if (!collection.Any())
            Assert.Fail(CreateMessage($"'{collectionName}' is empty"));
    }

    public void SequenceEqual<T>(
        IEnumerable<T> expected,
        IEnumerable<T> actual,
        IEqualityComparer<T>? equalityComparer = null,
        string? message = null
    )
    {
        var areEqual = actual.SequenceEqual(
            expected,
            equalityComparer ?? EqualityComparer<T>.Default
        );

        if (!areEqual)
            Assert.Fail(CreateMessage(message ?? "Sequences are not equal"));
    }

    public IVerifier PushContext(string context) => new TUnitVerifier(_context.Push(context));

    private string CreateMessage(string message) =>
        _context.Aggregate(
            message,
            (current, frame) => "Context: " + frame + Environment.NewLine + current
        );
}
