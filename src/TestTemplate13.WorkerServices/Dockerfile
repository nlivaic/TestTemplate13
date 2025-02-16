#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TestTemplate13.WorkerServices/TestTemplate13.WorkerServices.csproj", "src/TestTemplate13.WorkerServices/"]
COPY ["src/TestTemplate13.Core/TestTemplate13.Core.csproj", "src/TestTemplate13.Core/"]
COPY ["src/TestTemplate13.Common/TestTemplate13.Common.csproj", "src/TestTemplate13.Common/"]
RUN dotnet restore "src/TestTemplate13.WorkerServices/TestTemplate13.WorkerServices.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate13.WorkerServices"
RUN dotnet build "TestTemplate13.WorkerServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate13.WorkerServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/TestTemplate13.WorkerServices/entrypoint.sh", "src/entrypoint.sh"]
COPY ["src/TestTemplate13.WorkerServices/wait-for-it.sh", "src/wait-for-it.sh"]
CMD ["/bin/bash", "src/entrypoint.sh"]
