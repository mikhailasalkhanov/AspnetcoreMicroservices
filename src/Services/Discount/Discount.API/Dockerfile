FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Diccount.API/Discount.API.csproj", "Services/Discount/Diccount.API/"]
RUN dotnet restore "Services/Discount/Discount.API/Discount.API.csproj"
COPY . .
WORKDIR "/src/Diccount.API"
RUN dotnet build "Diccount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Diccount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Diccount.API.dll"]
