FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["mind-your-money-server/mind-your-money-server.csproj", "mind-your-money-server/"]
RUN dotnet restore "mind-your-money-server/mind-your-money-server.csproj"
COPY . .
WORKDIR "/src/mind-your-money-server"
RUN dotnet build "mind-your-money-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mind-your-money-server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mind-your-money-server.dll"]
