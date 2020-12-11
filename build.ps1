[string]$scriptDir = split-path -parent $MyInvocation.MyCommand.Definition

[string[]]$slnFiles = (join-path $scriptDir src\FindWords.sln)
[string[]]$testProjects = (Join-Path $scriptDir src\FindWords.Test\FindWords.Test.csproj)

[string]$buildConfig = 'Release'

function BuildAndTest{
    [cmdletbinding()]
    param(
        [string[]]$slnFilesToBuild = $slnFiles,
        [string[]]$testProjectsToExecute = $testProjects
    )
    process{

        foreach($sln in $slnFilesToBuild){
            'building solution file at ''{0}''' -f $sln | Write-Output
            dotnet restore $sln
            dotnet build $sln
        }

        foreach($test in $testProjectsToExecute){
            'running tests in project ''{0}''' -f $test | Write-Output
            dotnet test $test
        }
    }
}


BuildAndTest