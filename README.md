# jasondel.Tools

Shared logging tool used in many of my dotnet core projects.

Usage: 
~~~ 
Logger.Log("Hello, World");
Logger.Log("An error occurred", new ApplicationException("Bad thing"));

~~~

Outputs to stdout:
~~~bash
[25/Mar/2020 08:55:59.39 -04:00, Program:Main] Hello, World
[25/Mar/2020 08:57:40.99 -04:00, Program:Main] An error occured.
        ERROR: Bad thing
~~~

