@echo off
REM Current project uses .NET 7.0. If you don't have it installed, you need to install it.

REM If this script fails to run because a nuget package cannot be found, try this command: `dotnet nuget locals all --clear`

REM get the directory of this script
REM https://stackoverflow.com/questions/59895/how-do-i-get-the-directory-where-a-bash-script-is-located-from-within-the-script
SET THIS_DIR=%~dp0

REM you might need to restore and build the project before running it
REM dotnet restore %THIS_DIR%..\TranspileRunner\TranspileRunner.csproj
REM dotnet build %THIS_DIR%..\TranspileRunner\TranspileRunner.csproj
dotnet run --property WarningLevel=0 --project %THIS_DIR%..\TranspileRunner\TranspileRunner.csproj

