# CloudCustomers API

This project is for learning purposes only.

References: https://www.youtube.com/watch?v=ULJ3UEezisw

Using VSCode.

## Create Project

```console
dotnet new webapi -o CloudCustomers.API
dotnet new xunit -o CloudCustomers.UnitTests
```

## Build Setup
Create build.proj file
```xml
<Project Sdk="Microsoft.Build.Traversal/3.0.3">
    <ItemGroup>
        <ProjectReference Include="**\*.*proj" />
    </ItemGroup>
</Project>
```

Open CloudCustomers.API with VSCode and let it auto-generate assets for build and debug.

```console
cd CloudCustomers.API
code .
```
Ctrl+Shift+P => .NET: Generate Assets for Build and Debug

Move .vscode folder to parent directory and replace {workspaceFolder} for {workspaceFolder}/CloudCustomers.API on launch.json and tasks.json files.

```json
tasks.json
{
    "label": "build",
    "command": "dotnet",
    "type": "process",
    "args": [
        "build",
        "${workspaceFolder}/build.proj",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
    ],
    "problemMatcher": "$msCompile",
    "group": {
        "kind": "build",
        "isDefault": true
    }
}
```

## Test Dependencies

```console
cd CloudCustomers.UnitTests
dotnet add reference ../CloudCustomers.API/CloudCustomers.API.csproj
dotnet add package moq
dotnet add package FluentAssertions
```

## Dev Cert
```console
dotnet dev-certs https --trust
```