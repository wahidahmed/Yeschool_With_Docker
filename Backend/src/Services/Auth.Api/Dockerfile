#  STAGE 1: Base (lightweight runtime image) (Base stage)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS="http://+:80"

# STAGE 2: Build (SDK image for compiling)  (Build stage)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj file
COPY ["Auth.Api.csproj","./"]
RUN dotnet restore "Auth.Api.csproj"

# #copy full source  backend/src context
# COPY Yeschool/Backend/src/. ./

# Copy all source code (controllers, models, etc.)
COPY . .

FROM build AS publish
RUN dotnet publish "Auth.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","Auth.Api.dll" ]