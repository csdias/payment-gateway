Param
(
    [Parameter(Mandatory = $false)]  [switch] $SkipTests = $false,
    [Parameter(Mandatory = $false)]  [switch] $RunDockerTests = $false
)

Set-StrictMode -Version Latest -Verbose
$ErrorActionPreference = "stop"

function Write-Parameters {
    Write-Host "|Resolved Parameters:"
    Write-Host "+-----------------------------------------------------------------------------------"
    Write-Host "|         SKIP_TESTS: $SkipTests"
    Write-Host "|   RUN_DOCKER_TESTS: $RunDockerTests"
    Write-Host "+-----------------------------------------------------------------------------------"
}

function ToolsInfo {
    Write-Host "| TOOLS INFO"
    dotnet --info
    if ($LASTEXITCODE -ne 0) { throw 'dotnet failed' }
    dotnet tool update amazon.lambda.tools --tool-path $pwd/tool
    if ($LASTEXITCODE -ne 0) { throw 'install/update aws lambda tools failed' }
    Write-Host "+----------------------------------------------------------------------------------------------"
}

function Clean {
    Write-Host "| CLEAN"

    if (Test-Path .\NugetPackage) {
        Remove-Item -Recurse -Force .\NugetPackage
    }
    New-Item -ItemType Directory .\NugetPackage

    if (Test-Path .\Publish) {
        Remove-Item -Recurse -Force .\Publish
    }
    New-Item -ItemType Directory .\Publish

    Remove-Item -Recurse -Force **\bin
    Remove-Item -Recurse -Force **\obj
    Remove-Item -Recurse -Force **\Publish

    dotnet clean .\paymentgateway.sln -c Release
    if ($LASTEXITCODE -ne 0) { throw 'Clean failed' }
    Write-Host "+----------------------------------------------------------------------------------------------"
}

function RestoreAndBuild {
    Write-Host "| BUILD"
    $copyright = "Copyright $([char]0x00a9) $(Get-Date -Format "yyyy"). All rights reserved."
    $company = "Acme"

    dotnet build .\paymentgateway.sln -c Release /p:Copyright=$copyright /p:Company=$company
    if ($LASTEXITCODE -ne 0) { throw 'Build failed' }
    Write-Host "+----------------------------------------------------------------------------------------------"
}

function PublishPaymentLambda {
    Write-Host "| PUBLISH PAYMENT LAMBDA"
    Write-Host "|   Creating archives"
    ./tool/dotnet-lambda package -pl FrameworksAndDrivers -o Publish\payment-gateway-api.zip
    if ($LASTEXITCODE -ne 0) { throw "Pack of Lambda failed" }

    Write-Host "+----------------------------------------------------------------------------------------------"
}

function PublishQueueProcessorLambda {
    Write-Host "| PUBLISH QUEUE PROCESSOR LAMBDA"
    Write-Host "|   Creating archives"
    ./tool/dotnet-lambda package -pl QueueProcessor -o Publish\queue-processor.zip
    if ($LASTEXITCODE -ne 0) { throw "Pack of Lambda failed" }

    Write-Host "+----------------------------------------------------------------------------------------------"
}

function DiscoverProjects {
    Write-Host "| DISCOVER PROJECTS"
    $projects = @()
    Get-ChildItem -Filter *.csproj -Recurse |

    ForEach-Object {
        [xml]$project = Get-Content $_.FullName

        $isNuget = $null -ne $project.SelectSingleNode('/Project/PropertyGroup/GeneratePackageOnBuild[text()="True"]')
        $isLambda = $null -ne $project.SelectSingleNode('/Project/PropertyGroup/AWSProjectType[text()="Lambda"]')

        $projects += [System.Tuple]::Create($_.BaseName, $isNuget, $isLambda, $_.FullName)
    }

    $projects.ForEach{
        Write-Host "Project: [" $_.Item1 "] Is Nuget? [" $_.Item2 "] Is Lambda? [" $_.Item3 "]"
    }

    Write-Host "+----------------------------------------------------------------------------------------------"
    return , $projects
}

function UnitTest {
    Write-Host "| UNIT_TEST"
    if ($SkipTests) {
        Write-Host "  SKIPPED"
    }
    else {
        $csproj = ".\tests\FrameworksAndDrivers.UnitTests.csproj"
        if ($RunDockerTests) {
            dotnet test $csproj -c Release --no-build --logger="trx"
        }
        else {
            dotnet test $csproj -c Release --no-build --logger="trx" --filter "RequiresDocker!=true"
        }
        if ($LASTEXITCODE -ne 0) { throw 'UnitTest failed' }
    }
    Write-Host "+----------------------------------------------------------------------------------------------"
}

$exitCode = 0

try {
    Write-Host "+----------------------------------------------------------------------------------------------"
    Write-Host "| STARTING Build"
    Write-Host "+----------------------------------------------------------------------------------------------"

    Write-Parameters

    ToolsInfo

    $Projects = DiscoverProjects

    Clean
    RestoreAndBuild
    UnitTest
    PublishPaymentLambda
    PublishQueueProcessorLambda

    Write-Host "+---------------------------------------------------------------------------------------------"
    Write-Host "| SUCCEEDED"
    Write-Host "+----------------------------------------------------------------------------------------------"
}
catch [Exception] {
    $exitCode = 1
    Write-Host "+------------------------- EXCEPTION CAUGHT ------------------------- "
    Write-Error $_.Exception -ErrorAction Continue | Format-List -Force
    Write-Host "+-------------------------------------------------------------------- "
}
finally {
    Write-Host "| Exited with code [$exitCode]"
    Write-Host "+-------------------------------------------------------------------- "
    exit $exitCode
}
