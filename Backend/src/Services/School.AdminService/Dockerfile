#base stage (runtime image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS="http://+:80"

#stage 2. build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src

#copy project file and restore
COPY ["School.AdminService.csproj","./"]
RUN dotnet restore "School.AdminService.csproj"

#copy full source
COPY . .

FROM build AS publish
RUN dotnet publish "School.AdminService.csproj" -c Release -o /app/publish --no-restore

FROM  base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","School.AdminService.dll" ]








