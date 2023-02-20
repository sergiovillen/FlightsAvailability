#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FlightsAvailability.Search.ResultsProcessor.Skyscanner/FlightsAvailability.Search.ResultsProcessor.Skyscanner.csproj", "FlightsAvailability.Search.ResultsProcessor.Skyscanner/"]
RUN dotnet restore "FlightsAvailability.Search.ResultsProcessor.Skyscanner/FlightsAvailability.Search.ResultsProcessor.Skyscanner.csproj"
COPY . .
WORKDIR "/src/FlightsAvailability.Search.ResultsProcessor.Skyscanner"
RUN dotnet build "FlightsAvailability.Search.ResultsProcessor.Skyscanner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlightsAvailability.Search.ResultsProcessor.Skyscanner.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightsAvailability.Search.ResultsProcessor.Skyscanner.dll"]