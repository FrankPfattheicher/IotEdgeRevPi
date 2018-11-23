#!/bin/sh

MODULE="iotregistry.azurecr.io/edgeledmodule:0.0.17-amd64"

docker build --rm -f ./Dockerfile.amd64.debug -t $MODULE .
docker push $MODULE
