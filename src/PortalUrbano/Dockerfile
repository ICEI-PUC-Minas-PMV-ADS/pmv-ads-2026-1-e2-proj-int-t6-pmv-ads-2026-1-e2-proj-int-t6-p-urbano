# Estágio 1: Compilação e publicação
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar arquivo de projeto e restaurar dependências
COPY ["portal_urbano.csproj", "./"]
RUN dotnet restore "portal_urbano.csproj"

# Copiar todo o resto e realizar o build/publish em modo Release
COPY . .
RUN dotnet publish "portal_urbano.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 2: Ambiente de execução
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Variável de ambiente padrão para o ASP.NET Core escutar na porta correta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "portal_urbano.dll"]
