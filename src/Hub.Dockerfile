FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV ASPNETCORE_URLS=http://*:5276
EXPOSE 5276

# Build Projects
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Hub
WORKDIR "/src"
COPY ["./DBViewer.Hub/.", "./DBViewer.Hub"]
COPY ["./DbViewer.Shared/.", "./DbViewer.Shared"]

# dotnet
RUN dotnet restore "./DBViewer.Hub/DBViewer.Hub.csproj"
RUN dotnet build "./DBViewer.Hub/DBViewer.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./DBViewer.Hub/DBViewer.Hub.csproj" -c Release -o /app/publish

# copy build to final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DBViewer.Hub.dll"]