# Weather Station Dashboard
Repository with the Weather Station Dashboard solution.

These WebApp + APIs allow you to watch and search measurements taken by the device you built and configured following the [Sensors Reader Project](https://github.com/weather-station-project/sensors-reader)

## Release information
[![codecov](https://codecov.io/gh/weather-station-project/dashboard/branch/master/graph/badge.svg?token=59OP3KE0AA)](https://codecov.io/gh/weather-station-project/dashboard)
[![Dockerhub](https://img.shields.io/badge/dockerhub-v1.0.0-blue)](https://hub.docker.com/repository/docker/weatherstationproject/dashboard)

## ARQ Diagram
![ARQ diagram](https://raw.githubusercontent.com/weather-station-project/dashboard/master/dashboard-arq.png)

## Usage
### Create your own certificate for the WebApp
The WebApp and the APIs are designed to be run with https support against the port 1443. To achieve this, it is necessary to create a certificate and it must be hosted
in a place accessible by the docker container.
[Here](https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#create-a-self-signed-certificate), you have a 
tutorial to create a .pfx file protected by password.

### Pre-requisites
* The device needs a LAN connection, either Wi-Fi or wired, to the database used by the Sensors application.
* A .pfx certificate file created following the steps above and accessible by the Docker server..

### Execution parameters
* **AUTHENTICATION_SECRET** - Password to be used by the WebApp to get a token to make authenticated request to the Gateway. It is used also by
the AuthenticationService to compare the value passed by anybody who wants to obtain a token.
* **AUDIENCE_SECRET** - Secret shared by the APIs and the Gateway to manage authenticated requests. The value must be the same in all the services.
* **ASPNETCORE_Kestrel__Certificates__Default__Password** - Password of the .pfx file.
* **ASPNETCORE_Kestrel__Certificates__Default__Path** - Absolute path to the .pfx file. It is important that it is mounted and NOT stored in the container due to security reasons.
* **SERVER** - Server with the PostgreSQL database.
* **DATABASE** - PostgreSQL database name.
* **USER** - User to connect to the PostgreSQL database.
* **PASSWORD** - Password to connect to the PostgreSQL database.

A docker-compose example is provided with the solution code.

### Example of docker-compose file with basic configuration
```YAML
version: '3.5'
services:
  authentication-service:
    container_name: authentication-service
    image: weatherstationproject/authentication-service
    ports:
      - "3443:1443"
    environment:
      - AUTHENTICATION_SECRET=123456
      - AUDIENCE_SECRET=MySecret
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  gateway-service:
    depends_on:
      - authentication-service
      - ambient-temperature-service
      - air-parameter-service
      - wind-measurements-service
      - rainfall-service
      - ground-temperature-service
    container_name: gateway-service
    image: weatherstationproject/gateway-service
    ports:
      - "2443:1443"
    environment:
      - AUDIENCE_SECRET=MySecret
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  ambient-temperature-service:
    container_name: ambient-temperature-service
    image: weatherstationproject/ambient-temperature-service
    environment:
      - AUDIENCE_SECRET=MySecret
      - SERVER=127.0.0.1
      - DATABASE=database-name
      - USER=value
      - PASSWORD=value
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  air-parameter-service:
    container_name: air-parameter-service
    image: weatherstationproject/air-parameter-service
    environment:
      - AUDIENCE_SECRET=MySecret
      - SERVER=127.0.0.1
      - DATABASE=database-name
      - USER=value
      - PASSWORD=value
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  wind-measurements-service:
    container_name: wind-measurements-service
    image: weatherstationproject/wind-measurements-service
    environment:
      - AUDIENCE_SECRET=MySecret
      - SERVER=127.0.0.1
      - DATABASE=database-name
      - USER=value
      - PASSWORD=value
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  rainfall-service:
    container_name: rainfall-service
    image: weatherstationproject/rainfall-service
    environment:
      - AUDIENCE_SECRET=MySecret
      - SERVER=127.0.0.1
      - DATABASE=database-name
      - USER=value
      - PASSWORD=value
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  ground-temperature-service:
    container_name: ground-temperature-service
    image: weatherstationproject/ground-temperature-service
    environment:
      - AUDIENCE_SECRET=MySecret
      - SERVER=127.0.0.1
      - DATABASE=database-name
      - USER=value
      - PASSWORD=value
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
  dashboard:
    depends_on:
      - gateway-service
    container_name: dashboard
    image: weatherstationproject/dashboard
    ports:
      - "1443:1443"
    environment:
      - AUTHENTICATION_SECRET=123456
      - ACCUWEATHER_LOCATION_NAME=Moralzarzal
      - WEATHER_API_HOST=https://gateway-service:1443
      - ACCUWEATHER_API_KEY=agzy4UX4RlokbgSksCKR4ZVbcFAA6bvi
      - AUTHENTICATION_SERVICE_HOST=https://authentication-service:1443
      - ASPNETCORE_Kestrel__Certificates__Default__Password=123456
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - /etc/timezone:/etc/timezone:ro
      - /etc/localtime:/etc/localtime:ro
      - /path_to_the_pfx_folder:/https:ro
```

## Change-log
* **v1.1.0** - Added historical data pages.
* **v1.0.0** - First version with current data page.
* **v0.1.0** - Some mockups with the design of the different pages.

## License
Use this code as you wish! Totally free to be copied/pasted.

## Donation
If you liked the repository, you found it useful and you are willing to contribute, don't hesitate! I will be very
grateful! :-)

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=4TFR2PQ2J3KLA&item_name=If+you+liked+the+project+and+you+are+willing+to+contribute%2C+don%27t+hesitate%21+I+will+be+very+grateful%21+%3A-%29&currency_code=EUR)
