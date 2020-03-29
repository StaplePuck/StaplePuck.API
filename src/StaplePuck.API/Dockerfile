FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/StaplePuck.API/StaplePuck.API.csproj", "src/StaplePuck.API/"]
COPY ["src/StaplePuck.Data/StaplePuck.Data.csproj", "src/StaplePuck.Data/"]
COPY ["src/StaplePuck.Core/StaplePuck.Core.csproj", "src/StaplePuck.Core/"]
RUN dotnet restore "src/StaplePuck.API/StaplePuck.API.csproj"
COPY . .
WORKDIR "/src/src/StaplePuck.API"
RUN dotnet build "StaplePuck.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StaplePuck.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StaplePuck.API.dll"]