FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["CITLogs/UI_Divider/UI_Divider.csproj", "UI_Divider/"]
COPY ["CITLogs/Manager/Manager.csproj", "Manager/"]
RUN dotnet restore "UI_Divider/UI_Divider.csproj"
COPY . .
RUN dotnet build "UI_Divider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UI_Divider.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/UI_Divider/dist .
COPY nginx.conf /etc/nginx/nginx.conf