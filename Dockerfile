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
WORKDIR "/src/CITLogs/UI_Divider"
RUN dotnet build "UI_Divider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UI_Divider.csproj" -c Release -o /app/publish

FROM nginx:1.18.0-alpine
WORKDIR /var/www/web
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80