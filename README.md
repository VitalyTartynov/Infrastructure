# Infrastructure
[![Build status](https://ci.appveyor.com/api/projects/status/y04xvpyugwc2wkbc/branch/master?svg=true)](https://ci.appveyor.com/project/VitalyTartynov/infrastructure/branch/master) [![Build status](https://ci.appveyor.com/api/projects/status/y04xvpyugwc2wkbc/branch/develop?svg=true)](https://ci.appveyor.com/project/VitalyTartynov/infrastructure/branch/develop)

## What is this project?
`Infrastructure` is a bunch of C# classes that can be useful for many different projects. It can simplify some common tasks, such as:
- Initializing your applications' code on startup (e.g. configuring logging, setting up error handlers, connecting to database, etc.) and uninitializing it on shutdown;
- Accessing data from embedded resources;
- Creating and auto-removing temporary files and folders;
- ...and more!

## What's in there?
This project contains the following assemblies:
- `Infrastructure.EmbeddedResources` - contains class that lets you access embedded resources;
- `Infrastructure.Initializer` - classes that help to initialize and uninitialize code of your application;
- `Infrastructure.Universal` - other useful classes to manage temporary files, get path to executing assembly, etc.

Each of these assemblies also has an assembly with unit tests. Test assemblies have the same name and suffix `.Tests` (e.g. `Infrastructure.Universal.Tests`).

For more information, please refer to `README.md` in project's directory.

## How to get it?
`Infrastructure` is published as a set of NuGet packages:
- [Infrastructure.EmbeddedResource](https://www.nuget.org/packages/Infrastructure.EmbeddedResources/)
- [Infrastructure.Initializer](https://www.nuget.org/packages/Infrastructure.Initializer/)
- [Infrastructure.Universal](https://www.nuget.org/packages/Infrastructure.Universal/)

All packages are targeting .NET Framework 4.6.1.

## How to build it?
To build this project, you will need:
- [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) (any edition will do);
- Access to [nuget.org](http://nuget.org).

## License
`Infrastructure` is licensed under the MIT license.