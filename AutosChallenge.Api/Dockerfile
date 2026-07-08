FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AutosChallenge.Api/AutosChallenge.Api.csproj", "AutosChallenge.Api/"]
COPY ["AutosChallenge.Core/AutosChallenge.Core.csproj", "AutosChallenge.Core/"]
COPY ["AutosChallenge.Infrastructure/AutosChallenge.Infrastructure.csproj", "AutosChallenge.Infrastructure/"]
RUN dotnet restore "./AutosChallenge.Api/AutosChallenge.Api.csproj"
COPY . .
WORKDIR "/src/AutosChallenge.Api"
RUN dotnet build "./AutosChallenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AutosChallenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutosChallenge.Api.dll"]
