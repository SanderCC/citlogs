FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
COPY ["CITLogs/UI_Divider/UI_Divider.csproj", "UI_Divider/"]
COPY ["CITLogs/Manager/Manager.csproj", "Manager/"]
WORKDIR /src/CITLogs/UI_Divider
FROM build AS publish
RUN dotnet publish "UI_Divider/UI_Divider.csproj" -c Release -o output

FROM nginx:1.19.9-alpine
WORKDIR /var/www/web
COPY --from=publish /app/output/wwwroot .
# COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80