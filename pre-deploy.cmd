dotnet restore

dotnet clean --configuration Debug
dotnet clean --configuration Release

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\test\TauCode.Db.Testing.Tests\TauCode.Db.Testing.Tests.csproj
dotnet test -c Release .\test\TauCode.Db.Testing.Tests\TauCode.Db.Testing.Tests.csproj

nuget pack nuget\TauCode.Db.Testing.nuspec