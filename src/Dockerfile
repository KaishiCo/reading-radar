FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine as build
WORKDIR /src
COPY . .
RUN dotnet restore --disable-parallel
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ReadingRadar.Api.dll"]
