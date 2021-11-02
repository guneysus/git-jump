function build {
  dotnet build src\GitJump.csproj -c Release
}

function pack {
  dotnet pack
}

function push {
    .\nuget.exe push .\nupkg\*.nupkg $env:MYGET_API_KEY -source $env:MYGET_URL -skipduplicate
}