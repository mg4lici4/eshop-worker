# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore EShop.Worker/EShop.Worker.csproj
RUN dotnet publish EShop.Worker/EShop.Worker.csproj -c Release -o /out

# Etapa final: runtime limpio
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "EShop.Worker.dll"]