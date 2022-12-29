FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/OcelotApiGtw/OcelotApiGtw.csproj", "ApiGateways/OcelotApiGtw/"]
RUN dotnet restore "ApiGateways/OcelotApiGtw/OcelotApiGtw.csproj"
COPY . .
WORKDIR "/src/ApiGateways/OcelotApiGtw"
RUN dotnet build "OcelotApiGtw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApiGtw.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGtw.dll"]
