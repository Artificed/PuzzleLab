FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./src/PuzzleLab.Web/PuzzleLab.Web.csproj", "./PuzzleLab.Web/"]
COPY ["./src/PuzzleLab.Shared/PuzzleLab.Shared.csproj", "./PuzzleLab.Shared/"]
RUN dotnet restore "PuzzleLab.Web/PuzzleLab.Web.csproj"

COPY src/PuzzleLab.Web/ ./PuzzleLab.Web/
COPY src/PuzzleLab.Shared/ ./PuzzleLab.Shared/

WORKDIR "/src/PuzzleLab.Web"
RUN dotnet build "./PuzzleLab.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PuzzleLab.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PuzzleLab.Web.dll"]