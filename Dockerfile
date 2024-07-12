FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Clean_Architecture.sln Clean_Architecture.sln

COPY Restaurants.API/Restaurants.API.csproj Restaurants.API/Restaurants.API.csproj

COPY Restaurants.Application/Restaurants.Application.csproj Restaurants.Application/Restaurants.Application.csproj
COPY Restaurants.Domain/Restaurants.Domain.csproj Restaurants.Domain/Restaurants.Domain.csproj
COPY Restaurants.Infrastructure/Restaurants.Infrastructure.csproj Restaurants.Infrastructure/Restaurants.Infrastructure.csproj

RUN dotnet restore Restaurants.API/Restaurants.API.csproj
RUN dotnet restore Restaurants.Domain/Restaurants.Domain.csproj
RUN dotnet restore Restaurants.Application/Restaurants.Application.csproj
RUN dotnet restore Restaurants.Infrastructure/Restaurants.Infrastructure.csproj



COPY /Restaurants.API ./Restaurants.API
COPY /Restaurants.Application ./Restaurants.Application
COPY /Restaurants.Domain ./Restaurants.Domain
COPY /Restaurants.Infrastructure ./Restaurants.Infrastructure
WORKDIR /src/Restaurants.API
RUN dotnet publish Restaurants.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Restaurants.API.dll"]
