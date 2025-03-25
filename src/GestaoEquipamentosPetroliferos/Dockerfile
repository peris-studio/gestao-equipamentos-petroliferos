FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
LABEL maintainer="Hisllaylla Kézia"
WORKDIR /app
EXPOSE 8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo de projeto e restaurar as dependências
COPY src/GestaoEquipamentosPetroliferos/GestaoEquipamentosPetroliferos.csproj GestaoEquipamentosPetroliferos/
RUN dotnet restore "GestaoEquipamentosPetroliferos/GestaoEquipamentosPetroliferos.csproj"

# Copiar o código restante
COPY src .

# Compilar a aplicação
WORKDIR "/src/GestaoEquipamentosPetroliferos"
RUN dotnet build "GestaoEquipamentosPetroliferos.csproj" -c Release -o /app/build

# Etapa de publicação: publicar a aplicação para produção
FROM build AS publish
RUN dotnet publish "GestaoEquipamentosPetroliferos.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar a aplicação publicada
COPY --from=publish /app/publish .

# Configuração para rodar na porta 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Entrypoint para rodar a aplicação
ENTRYPOINT ["dotnet", "GestaoEquipamentosPetroliferos.dll"]