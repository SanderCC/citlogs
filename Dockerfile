FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base #								1/18
WORKDIR /app #																	2/18
EXPOSE 80 #																		3/18
EXPOSE 443 #																	4/18

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build #								5/18
WORKDIR /src #																	6/18
COPY ["CITLogs/Manager/Manager.csproj", "Manager/"] #							7/18
COPY ["CITLogs/UI_Divider/UI_Divider.csproj", "UI_Divider/"] #					8/18
RUN dotnet restore "UI_Divider/UI_Divider.csproj" #								9/18
COPY . . #																		10/18
WORKDIR "/src/UI_Divider" #														11/18
RUN dotnet build "UI_Divider/UI_Divider.csproj" -c Release -o /app/build #		12/18

FROM build AS publish #															13/18
RUN dotnet publish "UI_Divider.csproj" -c Release -o /app/publish #				14/18

FROM base AS final #															15/18
WORKDIR /app #																	16/18
COPY --from=publish /app/publish . #											17/18
ENTRYPOINT ["dotnet", "UI_Divider.dll"] #										18/18
