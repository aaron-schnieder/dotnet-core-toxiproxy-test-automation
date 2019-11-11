# .Net Core Toxiproxy Test Automation
This repo provides test automation examples using shopify/toxiproxy to create network level chaos and ensure services are able to handle various connectivity problems. The test automation is written in .net core and runs across various platforms. The intention of this repo is a best practice reference to use as a pattern to setup test automation for applications and APIs that rely on network connectivity for HTTP Rest API calls.

## Overview
The test automation example consists of the following projects.

### ToxiproxyDotNetCore
Console application that has an example HTTP Rest API client and implements dependency injection to enable test automation.

### ToxiproxyDotNetCore.Test.Api
Web Api that provides a simple HTTP Rest API that mirrors the APIs provided by postman-echo.com. This enables a local instance of the HTTP Rest API to run and local integration tests using toxiproxy toxics to execute with a controlled local endpoint.

### ToxiproxyDotNetCore.Test
Test automation that runs a set of unit and integration tests against the funtionality in the ToxiproxyDotNetCore console application.

### Test Architecture
The following diagram provides a visual overview of the services and interactions of the test automation.

![Test Automation Overview](test-automation-architecture.png)

## Running Toxiproxy
Toxiproxy must run as a local application or service to provide proxy functionality for network connections. There are many options to run Toxiproxy on your local OS or on your CI build server.

### Running as a Docker container

#### Pull the toxiproxy image
```
docker pull shopify/toxiproxy
```

#### Run an instance of the container
```
docker run --name=toxiproxy --net=host --rm --expose 8080 -p 8080:8080 -it shopify/toxiproxy
```

#### Exce into the container instance so we can run the CLI and setup proxies and toxics 
```
docker exec -it toxiproxy /bin/sh
cd /go/bin/
```

### Running as a Mac OS application

#### Brew install toxiproxy
```
brew tap shopify/shopify
brew install toxiproxy
```

#### Start the toxiproxy-server application
```
toxiproxy-server
```

### Running as a Windows 10 application
TODO: document running on Windows 10

## Testing Toxiproxy

### Create a new toxyproxy proxy with the name example, listening on localhost port 8080 and forwarded requests upstream to postman-echo.com port 80
```
toxiproxy-cli create example -l 127.0.0.1:8080 -u postman-echo.com:80
```

### Test our Toxiproxy proxy
```
curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2"
```

## Injecting Toxics

https://github.com/Shopify/toxiproxy#toxics


### Latency Example

```
toxiproxy-cli inspect example
toxiproxy-cli toxic add example -n latencyToxic -t latency -a latency=1000
date && curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2" && date
toxiproxy-cli toxic update example -n latencyToxic -a latency=5000
date && curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2" && date
toxiproxy-cli toxic delete example -n latencyToxic
toxiproxy-cli inspect example
```