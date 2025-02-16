#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TestTemplate13.Api/TestTemplate13.Api.csproj", "src/TestTemplate13.Api/"]
COPY ["src/TestTemplate13.Data/TestTemplate13.Data.csproj", "src/TestTemplate13.Data/"]
COPY ["src/TestTemplate13.Common/TestTemplate13.Common.csproj", "src/TestTemplate13.Common/"]
COPY ["src/TestTemplate13.Core/TestTemplate13.Core.csproj", "src/TestTemplate13.Core/"]
RUN dotnet restore "src/TestTemplate13.Api/TestTemplate13.Api.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate13.Api"
RUN dotnet build "TestTemplate13.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate13.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTemplate13.Api.dll"]