FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./src/PuzzleLab.Web/PuzzleLab.Web.csproj", "./PuzzleLab.Web/"]
COPY ["./src/PuzzleLab.Infrastructure/PuzzleLab.Infrastructure.csproj", "./PuzzleLab.Infrastructure/"]
COPY ["./src/PuzzleLab.Domain/PuzzleLab.Domain.csproj", "./PuzzleLab.Domain/"]
COPY ["./src/PuzzleLab.Application/PuzzleLab.Application.csproj", "./PuzzleLab.Application/"]

RUN dotnet restore "./PuzzleLab.Web/PuzzleLab.Web.csproj"

COPY ["./src/PuzzleLab.Web/", "./PuzzleLab.Web/"]
COPY ["./src/PuzzleLab.Infrastructure/", "./PuzzleLab.Infrastructure/"]
COPY ["./src/PuzzleLab.Domain/", "./PuzzleLab.Domain/"]
COPY ["./src/PuzzleLab.Application/", "./PuzzleLab.Application/"]

WORKDIR "/src/PuzzleLab.Web"
RUN dotnet build "PuzzleLab.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/PuzzleLab.Web
RUN dotnet publish "PuzzleLab.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PuzzleLab.Web.dll"]