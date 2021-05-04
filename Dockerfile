# Pull down the image with .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0.202-alpine3.13-amd64 AS Build
LABEL maintainer="David Leon <david.leon.m@gmail.com>"

# Fetch and install Node.js LTS
# RUN curl --silent --location https://deb.nodesource.com/setup_lts.x | /bin/ash -
RUN apk add --no-cache install nodejs npm

# Copy the source from your machine onto the container.
WORKDIR /Code/src/WeatherStationProject.App
COPY . .

# Install dependencies.
RUN dotnet restore "./WeatherStationProject.App.csproj"

# Compile, then pack the compiled app and dependencies into a deployable unit.
RUN dotnet publish "./WeatherStationProject.App.csproj" -c Release -o /app/publish

# Pull down the image which includes only the ASP.NET core runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.5-alpine3.13-amd64

# Expose port 80 and 443 for http(s) access.
EXPOSE 443
EXPOSE 80

# Copy the published app to this new runtime-only container.
COPY --from=Build /app/publish /app

# Change working directory to the app binaries
WORKDIR /app

# Configure the health check command
# HEALTHCHECK --interval=60s --start-period=60s CMD ["python", "-u", "-m", "health_check.health_check"] || exit 1

# To run the app, run `dotnet sample-app.dll`, which we just copied over.
ENTRYPOINT ["dotnet", "WeatherStationProject.App.dll"]
