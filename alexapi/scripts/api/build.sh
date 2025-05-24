dotnet restore alexapi/AlexAPI/AlexAPI.csproj
dotnet publish alexapi/AlexAPI/AlexAPI.csproj -c Release -r linux-x64 --self-contained false -o ./alexapi/AlexAPI/publish/