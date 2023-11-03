FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["DSCC_API.csproj", "./"]
RUN dotnet restore "DSCC_API.csproj"
COPY . .
RUN dotnet build "DSCC_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DSCC_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSCC_API.dll"]
