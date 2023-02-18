#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FlightsAvailability.Search.Agent.Amadeus/FlightsAvailability.Search.Agent.Amadeus.csproj", "FlightsAvailability.Search.Agent.Amadeus/"]
RUN dotnet restore "FlightsAvailability.Search.Agent.Amadeus/FlightsAvailability.Search.Agent.Amadeus.csproj"
COPY . .
WORKDIR "/src/FlightsAvailability.Search.Agent.Amadeus"
RUN dotnet build "FlightsAvailability.Search.Agent.Amadeus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlightsAvailability.Search.Agent.Amadeus.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightsAvailability.Search.Agent.Amadeus.dll"]