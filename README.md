
# Install Azure IoT-Edge on RevolutionPi (Core 3)
This is a documentation how to use Azure IoT Edge on a Revolution Pi (Core 3).

It is still work in progress. Please give feedback and ask question if something is not as clear as expected or totally wrong.

Feel free to create an issue, write a mail or even call per phone!

[For more information see the list of links to external resources...](Links.md)

## The Challange
The Revolution Pi is based on a Raspberry Pi Compute Module 3.    
This hardware has in contrast to the *full* Raspberr Pi 3 no Micro-SD-Card slot. It has 4GB of eMMC Flash directly soldered to the module. So we unfortunately can not upgrade this.

Due to the fact Azure IoT-Edge is based on Docker, which is not the problem, the typical size of docker images is. The typical size ranges from 100MB to 300MB.

The provided installation for the Revolution Pi is filled with all goodies available to start an automation project. We have to get rid of many of this to have enough room for our installation.

## The Benefit
Azure IoT-Hub with IoT-Edge is a great environment to run a bunch of devices.

* device management
* software distribution
* configuration update
* transferring data from devices to the server (Azure IoT-Hub)
* remote execution fo methods on the device (Direct Method Call)

## Finally - The Installation
The installation requires several steps to be done. Sometimes it is not possible to follow the documentation step-by-step because there are things to be done *between*. I will try to keep everything in a meaningful sequence.

1. [Prepare Installation](Prepare_Installation.md)
2. [Prepare USB Stick](Prepare_USB_Stick.md)
3. [Install IoT-Edge](Install_IoT-Edge.md)
4. [Reconfigure Docker](Reconfigure_Docker.md)
5. [Configure IoT-Edge](Configure_IoT-Edge.md)
6. [Run IoT-Edge](Run_IoT-Edge.md)

