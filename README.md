# DeRidder.Analyzers <img src="/img/logo.png?raw=true" width="128" align="right">
A collection of useful source analyzers for .NET

## Available Analyzers
| Analyzer               | Description                                                                  | Package                                                                                                                  |
|------------------------|------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------|
| DeRidder.Analyzers.Xml | Source analyzer suppressions for unavoidable issues when using XmlSerializer | [![Nuget](https://img.shields.io/nuget/v/DeRidder.Analyzers.Xml)](https://www.nuget.org/packages/DeRidder.Analyzers.Xml) |

### DeRidder.Analyzers.Xml Rules
| Rule ID  | Suppresses                                                                                         | Description                                                              |
|----------|----------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------|
| RID1002  | [CA1002](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1002) | Suppress CA1002 when the containing type is used by `XmlSerializer`      |