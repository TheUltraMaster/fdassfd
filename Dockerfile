
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["API.csproj", "./"]
RUN dotnet restore

# Copia todo el contenido del proyecto y compílalo
COPY . .
WORKDIR "/src/"
RUN dotnet build -c Release -o /app/build
EXPOSE 5000
EXPOSE 8080

# Publica la aplicación
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Usa la imagen de Runtime de .NET 8.0 para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]