# Overview

Shared logging tool used in many of my dotnet core projects.

## Usage: 

From you code, include the `jasondel.Tools` namespace and use:

```csharp
Logger.Log("Hello, World");
Logger.Log("An error occurred", new ApplicationException("Bad thing"));
```

Output is directed to stdout:
```bash
[25/Mar/2020 08:55:59.39 -04:00, Program:Main] Hello, World
[25/Mar/2020 08:57:40.99 -04:00, Program:Main] An error occured.
        ERROR: Bad thing
```

## Installation

A few special notes on how to reference this package from your dotnet project.  The package is published as Public to a github nuget package repository, however github requires that you still authenticate.  Use the Personal Access Token exactly as-is below, there are no sensitive secrets being shared here.

The easiest way is to add a `nuget.config` file like this in the same directory as your `.csproj` file.

~~~xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="github" value="https://nuget.pkg.github.com/delormej/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="delormej" />
            <add key="ClearTextPassword" value="4801520af89bba1bbbff0b25b9a40d5aa9655505" />
        </github>
    </packageSourceCredentials>
</configuration>
~~~

Then simply add the package:

```bash
dotnet package add jasondel.Tools
```

...or directly modify your csproj to add the reference:

```xml
    <PackageReference Include="jasondel.Tools" Version="1.0.*" />
```