dotnet restore

dotnet build TauCode.Db.Testing.sln -c Debug
dotnet build TauCode.Db.Testing.sln -c Release

dotnet test TauCode.Db.Testing.sln -c Debug
dotnet test TauCode.Db.Testing.sln -c Release

nuget pack nuget\TauCode.Db.Testing.nuspec