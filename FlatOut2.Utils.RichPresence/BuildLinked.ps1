# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/FlatOut2.Utils.RichPresence/*" -Force -Recurse
dotnet publish "./FlatOut2.Utils.RichPresence.csproj" -c Release -o "$env:RELOADEDIIMODS/FlatOut2.Utils.RichPresence" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location