FROM mcr.microsoft.com/dotnet/core/sdk:3.1.301-focal AS build-env

WORKDIR /app
COPY src/ ./

RUN dotnet restore
RUN dotnet build
RUN dotnet test
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.5-focal

WORKDIR /app

COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "Identity.Api.dll"]