#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
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