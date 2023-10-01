FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GameServicesCloud.UI/GameServicesCloud.UI.csproj", "GameServicesCloud.UI/"]
RUN dotnet restore "GameServicesCloud.UI/GameServicesCloud.UI.csproj"
COPY . .
WORKDIR "/src/GameServicesCloud.UI"
RUN dotnet build "GameServicesCloud.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameServicesCloud.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameServicesCloud.UI.dll"]
