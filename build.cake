//
var target = Argument("target", "ExecuteBuild");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./";
var solutionFile = "./WorkerServiceSolution.sln";
var projectFile = "./WorkerServiceDemo/WorkerServiceDemo.csproj";
var outputFolder = "./artifacts/publish";

Task("Clean")
    .Does(() => {
        CleanDirectory(outputFolder);
    });

Task("Restore")
    .Does(() => {
        DotNetCoreRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
            NoRestore = true,
            Configuration = configuration
        });       
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true
        });
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() => {
        DotNetCorePublish(projectFile, new DotNetCorePublishSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = outputFolder
        });
    });
        
Task("ExecuteBuild")
    .IsDependentOn("Publish");

RunTarget(target);