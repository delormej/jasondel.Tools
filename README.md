# Overview

Shared logging tool used in many of my dotnet core projects.

## Usage: 

From your code, include the `jasondel.Tools` namespace and use:

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

Detailed instructions are available [here](https://help.github.com/en/packages/using-github-packages-with-your-projects-ecosystem/configuring-dotnet-cli-for-use-with-github-packages#authenticating-to-github-packages) on using github packages with the dotnet cli. Even though this package is **public**, github requires that you still authenticate with *any* valid Personal Access Token. You will need to create and use your own Personal Access Token with **`read:package`** rights.

Once you have a Personal Access Token, create a `nuget.config` file like the one below in the same directory as your `.csproj` file.  Thankfully, you can reference environment variables in the file so you do not need to hardcode your Personal Access Token.  

1. Set your environment variable:

~~~bash
export GITHUB_TOKEN=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
~~~

2. Create a `nuget.config` file in the same directory as your `*.csproj`:

~~~xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="github" value="https://nuget.pkg.github.com/delormej/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="YOUR-github-user" />
            <add key="ClearTextPassword" value="%GITHUB_TOKEN%" />
        </github>
    </packageSourceCredentials>
</configuration>
~~~

3. Add the package to your project:

```bash
dotnet add package jasondel.Tools
```

...or directly modify your csproj to add the reference:

```xml
    <PackageReference Include="jasondel.Tools" Version="2.*" />
```
