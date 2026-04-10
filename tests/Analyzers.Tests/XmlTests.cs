namespace Analyzers.Tests;

internal sealed class XmlTests
{
    [Test]
    [Arguments(
        """
            using System.Collections.Generic;
            using System.Xml.Serialization;

            [XmlRoot]
            public class MyModel
            {
                [XmlArray]
                public List<string> Items { get; init; } = [];
            }
            """
    )]
    public async Task ReportOnPublicTypeWithConcreteList(
        string code,
        CancellationToken cancellationToken
    ) { }
}
