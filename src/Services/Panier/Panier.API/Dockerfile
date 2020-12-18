#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/Services/Panier/Panier.API/Panier.API.csproj", "src/Services/Panier/Panier.API/"]
RUN dotnet restore "src/Services/Panier/Panier.API/Panier.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Panier/Panier.API"
RUN dotnet build "Panier.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Panier.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Panier.API.dll"]