
# Install Azure IoT-Edge on RevolutionPi (Core 3)

## Prepare Installation
### purge non used programs
To free disk space purge some programs. There are 4GB only.

    sudo apt-get purge nodered
    sudo apt-get purge bluej
    sudo apt-get purge greenfoot
    sudo apt-get purge geany
    sudo apt-get purge python-pygame
    sudo apt-get purge python-sense-*
    sudo apt-get purge teamviewer-revpi
    sudo apt-get purge oracle-java8-jdk

Check available disk space.

    df -H

There should be 1.2G avail at least.

### Reduce GPU Memory
Reduce GPU Memory to 16 using Raspi-Configuration.

### Update system
This unfortunately will take back an amount of free disk space.

    sudo apt-get update
    sudo apt-get upgrade

## Install IoT-Edge
To install the runtime follow the steps in the Microsoft documentation:

https://docs.microsoft.com/de-de/azure/iot-edge/how-to-install-iot-edge-linux-arm

**Hint**
At the point "<ADD DEVICE CONNECTION STRING HERE>" - you cannot use the RasPi Browser to open the azure portal.
So use another machine to get the connection string and store it on a file share.

## Run IoT-Edge
Due to the edgeAgent requires to listen on port 443 we have to stop and disable the apache server.

    sudo systemctl stop apache2
    sudo systemctl disable apache2
    
Check that there is no other program listening on 443.

    sudo netstat -pln

## Reeconfigure Docker
During preperation for the installation we freed up a lot of disk space, 
but for the size of docker images this is not far enough!

So the only solution is, to store the docker images NOT here.

We therefore use an USB memory stick for that.

### Prepare Memory Stick
Plugged in tu an USB port the device is moutend automatically. To use the stick as desired we have to reformat it using the EXT4 file system. To do this execte the following steps.

    sudo umount /dev/sda1
    sudo mkfs.ext4 /dev/sda1 -L docker
    sudo mount /dev/sda1 /mnt/pi/docker

### Modify Docker Startup
To let docker use the memory stick, we have to set the base path to the stick's mount directory

