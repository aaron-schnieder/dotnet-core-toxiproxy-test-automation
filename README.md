
## Docker

### Pull the toxiproxy image
docker pull shopify/toxiproxy

### Run an instance of the container
docker run --name=toxiproxy --net=host --rm --expose 8080 -p 8080:8080 -it shopify/toxiproxy

### 
docker exec -it toxiproxy /bin/sh
/ cd /go/bin/

### Create a new toxy proxy with the name upstream listening on localhost port 111 and forwarded requests upstream to localhost port 80 
./toxiproxy-cli create example -l 127.0.0.1:8080 -u postman-echo.com:80

### 
curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2"

## Mac OS

brew tap shopify/shopify

brew install toxiproxy

toxiproxy-server

toxiproxy-cli create example -l 127.0.0.1:8080 -u postman-echo.com:80

curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2"


## Toxics

https://github.com/Shopify/toxiproxy#toxics


### Latency Example

toxiproxy-cli inspect example

toxiproxy-cli toxic add example -n latencyToxic -t latency -a latency=1000

date && curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2" && date

toxiproxy-cli toxic update example -n latencyToxic -a latency=5000

date && curl -v --location --request GET "127.0.0.1:8080/get?foo1=bar1&foo2=bar2" && date

toxiproxy-cli toxic delete example -n latencyToxic

toxiproxy-cli inspect example