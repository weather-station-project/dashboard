# Pull down the image with .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0.400-alpine3.13-amd64 AS Build
LABEL maintainer="David Leon <david.leon.m@gmail.com>"

# Arguments needed to parametrize the build
ARG INCLUDE_NPM
ARG PROJECT_NAME

# Install Node.JS if required
RUN if [[ "$INCLUDE_NPM" == "true" ]] ; then apk add --no-cache nodejs npm ; fi

# Copy the source and library projects from the repository onto the container and set it as working directory
COPY "/Code/src/$PROJECT_NAME" "/src/$PROJECT_NAME"
COPY "/Code/src/WeatherStationProject.Dashboard.Core" "/src/WeatherStationProject.Dashboard.Core"
COPY "/Code/src/WeatherStationProject.Dashboard.Data" "/src/WeatherStationProject.Dashboard.Data"
WORKDIR "/src/$PROJECT_NAME"

# Install dependencies
RUN if [[ "$INCLUDE_NPM" == "true" ]] ; then cd ClientApp && npm install --production ClientApp ; fi
WORKDIR "/src/$PROJECT_NAME"
RUN dotnet restore "./$PROJECT_NAME.csproj"

# Deploy the app and dependencies into a deployable unit
RUN dotnet publish "./$PROJECT_NAME.csproj" --configuration Release --output "/app/publish"

# Pull down the image which includes only the ASP.NET core runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.9-alpine3.13-amd64

# Expose port 80 and 443 for http(s) access
EXPOSE 443
EXPOSE 80

# Copy the published app to this new runtime-only container
COPY --from=Build "/app/publish" "/app"

# Change working directory to the app binaries
WORKDIR "/app"

# Configure the health check command
# HEALTHCHECK --interval=60s --start-period=60s CMD ["python", "-u", "-m", "health_check.health_check"] || exit 1

# Run the application
ENTRYPOINT ["dotnet", "$PROJECT_NAME.dll"]
