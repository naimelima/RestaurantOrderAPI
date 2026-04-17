
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["RestaurantOrderAPI/RestaurantOrderAPI.csproj", "RestaurantOrderAPI/"]
RUN dotnet restore "RestaurantOrderAPI/RestaurantOrderAPI.csproj"

COPY . .
WORKDIR "/src/RestaurantOrderAPI"
RUN dotnet publish "RestaurantOrderAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "RestaurantOrderAPI.dll"]