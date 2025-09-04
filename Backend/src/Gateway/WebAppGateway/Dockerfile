#base stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Yeschool/Backend/src/Gateway/WebAppGateway/WebAppGateway.csproj","Gateway/WebAppGateway"]
RUN dotnet restore "Gateway/WebAppGateway/WebAppGateway.csproj"

COPY ["Yeschool/Backend/src/.","./"]

WORKDIR "/src/Gateway/WebAppGateway"
RUN dotnet build "WebAppGateway.csproj" -c Release -o app/build

FROM build AS publish
RUN dotnet publish "WebAppGateway.csproj" -c Release -o app/publish

FROM base AS final
WORKDIR /app
COPY --from=build app/publish .
ENTRYPOINT [ "dotnet","WebAppGateway.dll" ]

