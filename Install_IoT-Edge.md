
## Install IoT-Edge
To install the runtime follow the steps in the Microsoft documentation:

https://docs.microsoft.com/de-de/azure/iot-edge/how-to-install-iot-edge-linux-arm

**Attention**    
Do not configure the service before reconfigured docker. Otherwise the service starts downloading images to the initial repository on the eMMC drive.

**Hint**    
At the point ```"<ADD DEVICE CONNECTION STRING HERE>"``` - you cannot use the RasPi Browser to open the azure portal. The portal requires a *modern* borowser to work.    
So use another machine to get the connection string and store it on a file share.

