FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./src/PuzzleLab.BackgroundWorker/PuzzleLab.BackgroundWorker.csproj", "./PuzzleLab.BackgroundWorker/"]
COPY ["./src/PuzzleLab.Infrastructure/PuzzleLab.Infrastructure.csproj", "./PuzzleLab.Infrastructure/"]
COPY ["./src/PuzzleLab.Domain/PuzzleLab.Domain.csproj", "./PuzzleLab.Domain/"]
COPY ["./src/PuzzleLab.Application/PuzzleLab.Application.csproj", "./PuzzleLab.Application/"]
COPY ["./src/PuzzleLab.Shared/PuzzleLab.Shared.csproj", "./PuzzleLab.Shared/"]

RUN dotnet restore "./PuzzleLab.BackgroundWorker/PuzzleLab.BackgroundWorker.csproj"

COPY ["./src/PuzzleLab.BackgroundWorker/", "./PuzzleLab.BackgroundWorker/"]
COPY ["./src/PuzzleLab.Infrastructure/", "./PuzzleLab.Infrastructure/"]
COPY ["./src/PuzzleLab.Domain/", "./PuzzleLab.Domain/"]
COPY ["./src/PuzzleLab.Application/", "./PuzzleLab.Application/"]
COPY ["./src/PuzzleLab.Shared/", "./PuzzleLab.Shared/"]

WORKDIR "/src/PuzzleLab.BackgroundWorker"
RUN dotnet build "PuzzleLab.BackgroundWorker.csproj" -c $BUILD_CONFIGURATION --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/PuzzleLab.BackgroundWorker"
RUN dotnet publish "PuzzleLab.BackgroundWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PuzzleLab.BackgroundWorker.dll"]