# Arguments needed to parametrize the build
ARG INCLUDE_NPM_ARG
ARG PROJECT_NAME_ARG
ARG ENVIRONMENT_ARG

# Pull down the image with .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0.400-alpine3.13-amd64 AS Build
LABEL maintainer="David Leon <david.leon.m@gmail.com>"

# Global args re-mapped for this stage
ARG INCLUDE_NPM_ARG
ARG PROJECT_NAME_ARG

# Install Node.JS if required
RUN if [[ "$INCLUDE_NPM_ARG" == "true" ]] ; then apk add --no-cache nodejs npm ; fi

# Copy the source and library projects from the repository onto the container and set it as working directory
COPY "/Code/src/$PROJECT_NAME_ARG" "/src/$PROJECT_NAME_ARG"
COPY "/Code/src/WeatherStationProject.Dashboard.Core" "/src/WeatherStationProject.Dashboard.Core"
COPY "/Code/src/WeatherStationProject.Dashboard.Data" "/src/WeatherStationProject.Dashboard.Data"
WORKDIR "/src/$PROJECT_NAME_ARG"

# Deploy the app and dependencies into a deployable unit
RUN dotnet publish "./$PROJECT_NAME_ARG.csproj" --configuration Release --output "/app/publish"

# Pull down the image which includes only the ASP.NET core runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.9-alpine3.13-amd64

# Global args re-mapped for this stage
ARG PROJECT_NAME_ARG
ARG ENVIRONMENT_ARG

# ENV variables from args to be used during app execution
ENV PROJECT_NAME=$PROJECT_NAME_ARG
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT_ARG

# Ports, URLS and certificate for http(s) access
EXPOSE 1443
ENV ASPNETCORE_URLS="https://+:1443" 

# Copy the published app to this new runtime-only container
COPY --from=Build "/app/publish" "/app"

# Change working directory to the app binaries
WORKDIR "/app"

# Configure the health check command
# HEALTHCHECK --interval=60s --start-period=60s CMD ["python", "-u", "-m", "health_check.health_check"] || exit 1

# Run the application
ENTRYPOINT ["ash", "-c", "dotnet $PROJECT_NAME.dll"]