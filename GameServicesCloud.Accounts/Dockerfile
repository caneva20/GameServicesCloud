FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GameServicesCloud.Accounts/GameServicesCloud.Accounts.csproj", "GameServicesCloud.Accounts/"]
RUN dotnet restore "GameServicesCloud.Accounts/GameServicesCloud.Accounts.csproj"
COPY . .
WORKDIR "/src/GameServicesCloud.Accounts"
RUN dotnet build "GameServicesCloud.Accounts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameServicesCloud.Accounts.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameServicesCloud.Accounts.dll"]
