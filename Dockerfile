FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CITLogs/Manager/Manager.csproj", "Manager/"]
COPY ["CITLogs/UI_Divider/UI_Divider.csproj", "UI_Divider/"]
RUN dotnet restore "UI_Divider/UI_Divider.csproj"
COPY . .
WORKDIR "/src/UI_Divider"
RUN dotnet build "UI_Divider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UI_Divider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UI_Divider.dll"]