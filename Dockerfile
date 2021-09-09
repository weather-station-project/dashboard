# Arguments needed to parametrize the build
ARG INCLUDE_NPM
ARG PROJECT_NAME

# Pull down the image with .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0.400-alpine3.13-amd64 AS Build
LABEL maintainer="David Leon <david.leon.m@gmail.com>"

# Variables from global args
ARG INCLUDE_NPM
ARG PROJECT_NAME
ENV INCLUDE_NPM_VAR=$INCLUDE_NPM
ENV PROJECT_NAME_VAR=$PROJECT_NAME

# Install Node.JS if required
RUN if [[ "$INCLUDE_NPM_VAR" == "true" ]] ; then apk add --no-cache nodejs npm ; fi

# Copy the source and library projects from the repository onto the container and set it as working directory
COPY "/Code/src/$PROJECT_NAME_VAR" "/src/$PROJECT_NAME_VAR"
COPY "/Code/src/WeatherStationProject.Dashboard.Core" "/src/WeatherStationProject.Dashboard.Core"
COPY "/Code/src/WeatherStationProject.Dashboard.Data" "/src/WeatherStationProject.Dashboard.Data"
WORKDIR "/src/$PROJECT_NAME_VAR"

# Install dependencies
RUN dotnet restore "./$PROJECT_NAME_VAR.csproj"

# Deploy the app and dependencies into a deployable unit
RUN dotnet publish "./$PROJECT_NAME_VAR.csproj" --configuration Release --output "/app/publish"

# Pull down the image which includes only the ASP.NET core runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.9-alpine3.13-amd64

# Variable from global args
ARG PROJECT_NAME
ENV PROJECT_NAME_VAR=$PROJECT_NAME

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
ENTRYPOINT ["dotnet", "$PROJECT_NAME_VAR.dll"]
