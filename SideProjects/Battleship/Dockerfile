FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /App

COPY ./ ./

RUN dotnet build

#Two Phase build

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /App

COPY --from=build /App/Battleship/bin/Debug/net6.0/ ./ 

ENTRYPOINT ["dotnet", "./Battleship.dll"]