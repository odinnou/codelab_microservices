#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/Services/Catalogue/Catalogue.API/Catalogue.API.csproj", "src/Services/Catalogue/Catalogue.API/"]
RUN dotnet restore "src/Services/Catalogue/Catalogue.API/Catalogue.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalogue/Catalogue.API"
RUN dotnet build "Catalogue.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalogue.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalogue.API.dll"]