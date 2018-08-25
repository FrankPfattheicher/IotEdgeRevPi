
## Prepare USB Stick


Plugged in to an USB port the device is moutend automatically. To use the stick as desired we have to reformat it using the EXT4 file system. To do this execte the following steps.

    sudo umount /dev/sda1
    sudo mkfs.ext4 /dev/sda1 -L docker
    sudo mount /dev/sda1 /mnt/pi/docker

