FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PuzzleLab.BackgroundService/PuzzleLab.BackgroundService.csproj", "src/PuzzleLab.BackgroundService/"]
RUN dotnet restore "src/PuzzleLab.BackgroundService/PuzzleLab.BackgroundService.csproj"
COPY . .
WORKDIR "/src/src/PuzzleLab.BackgroundService"
RUN dotnet build "PuzzleLab.BackgroundService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PuzzleLab.BackgroundService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PuzzleLab.BackgroundService.dll"]
