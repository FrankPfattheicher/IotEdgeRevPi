#!/bin/sh

MODULE="iotregistry.azurecr.io/edgeledmodule:0.0.13-arm32v7"

docker build --rm -f Dockerfile.arm32v7 -t $MODULE .
docker push $MODULE
