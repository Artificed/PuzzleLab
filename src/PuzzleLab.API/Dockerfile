FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./src/PuzzleLab.API/PuzzleLab.API.csproj", "./PuzzleLab.API/"]
COPY ["./src/PuzzleLab.Infrastructure/PuzzleLab.Infrastructure.csproj", "./PuzzleLab.Infrastructure/"]
COPY ["./src/PuzzleLab.Domain/PuzzleLab.Domain.csproj", "./PuzzleLab.Domain/"]
COPY ["./src/PuzzleLab.Application/PuzzleLab.Application.csproj", "./PuzzleLab.Application/"]

RUN dotnet restore "./PuzzleLab.API/PuzzleLab.API.csproj"

COPY ["./src/PuzzleLab.API/", "./PuzzleLab.API/"]
COPY ["./src/PuzzleLab.Infrastructure/", "./PuzzleLab.Infrastructure/"]
COPY ["./src/PuzzleLab.Domain/", "./PuzzleLab.Domain/"]
COPY ["./src/PuzzleLab.Application/", "./PuzzleLab.Application/"]

WORKDIR "/src/PuzzleLab.API"
RUN dotnet build "PuzzleLab.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/PuzzleLab.API
RUN dotnet publish "PuzzleLab.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PuzzleLab.API.dll"]