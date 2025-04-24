# Stage 1: сборка
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY dotnet_mysql.sln .  
COPY *.csproj ./  
RUN dotnet restore

COPY . .  
RUN dotnet publish -c Release -o out

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS="http://+:7070"
EXPOSE 7070

# Копируем собранное приложение
COPY --from=build /app/out .  
ENTRYPOINT ["dotnet", "dotnet_mysql.dll"]
