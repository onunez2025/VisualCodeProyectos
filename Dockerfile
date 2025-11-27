# Usa la imagen oficial de .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia el archivo del proyecto y restaura dependencias
COPY TicketsAPI/*.csproj ./TicketsAPI/
RUN dotnet restore ./TicketsAPI/TicketsAPI.csproj

# Copia el resto del código y compila
COPY TicketsAPI/. ./TicketsAPI/
WORKDIR /source/TicketsAPI
RUN dotnet publish -c Release -o /app

# Usa la imagen runtime de .NET 8 para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Railway proporciona el puerto dinámicamente a través de la variable PORT
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
ENV ASPNETCORE_ENVIRONMENT=Production

# Expone el puerto (Railway lo asignará automáticamente)
EXPOSE ${PORT:-8080}

ENTRYPOINT ["dotnet", "TicketsAPI.dll"]
