#!/bin/bash

# Current project uses .NET 7.0. If you don't have it installed, you can install it with the following commands:
# sudo apt-get update && sudo apt-get install -y dotnet-sdk-7.0

# If this script fails to run because a nuget package cannot be found, try this command: `dotnet nuget locals all --clear`

# exit when any command fails
set -e

# get the directory of this script
# https://stackoverflow.com/questions/59895/how-do-i-get-the-directory-where-a-bash-script-is-located-from-within-the-script
THIS_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

# dotnet restore $THIS_DIR/../TranspileRunner/TranspileRunner.csproj
# dotnet build $THIS_DIR/../TranspileRunner/TranspileRunner.csproj
dotnet run --property WarningLevel=0 --project $THIS_DIR/../TranspileRunner/TranspileRunner.csproj


