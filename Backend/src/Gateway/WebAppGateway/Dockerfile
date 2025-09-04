FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS="http://+:80"

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src

COPY "Backend/src/Gateway/WebAppGateway/WebAppGateway.csproj" "/Backend/src/Gateway/WebAppGateway/"
RUN dotnet restore "Backend/src/Gateway/WebAppGateway/WebAppGateway.csproj"

COPY . .

WORKDIR "/src/Backend/src/Gateway/WebAppGateway"
RUN dotnet build "WebAppGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppGateway.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","WebAppGateway.dll" ]


