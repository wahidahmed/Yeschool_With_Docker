# #base stage (runtime image)
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
# WORKDIR /app
# EXPOSE 80
# ENV ASPNETCORE_URLS="http://+:80"

# #stage 2. build stage
# FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
# WORKDIR /src

# #copy project file and restore
# COPY ["School.AdminService.csproj","./"]
# RUN dotnet restore "School.AdminService.csproj"

# #copy full source
# COPY . .

# FROM build AS publish
# RUN dotnet publish "School.AdminService.csproj" -c Release -o /app/publish --no-restore

# FROM  base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT [ "dotnet","School.AdminService.dll" ]


#=========the above code for indivitual project only==================

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS="http://+:80"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Backend/src/Services/School.AdminService/School.AdminService.csproj","./Backend/src/Services/School.AdminService/"]
RUN dotnet restore "Backend/src/Services/School.AdminService/School.AdminService.csproj"

COPY . .

WORKDIR "/src/Backend/src/Services/School.AdminService"
RUN dotnet build "School.AdminService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "School.AdminService.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","School.AdminService.dll" ]




