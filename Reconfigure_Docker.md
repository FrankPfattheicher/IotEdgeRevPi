
## Reconfigure Docker
During preperation for the installation we freed up a lot of disk space, 
but for the size of docker images this is not far enough!

So the only solution is, to store the docker images NOT here.

We therefore use an USB memory stick for that.

### Modify Configuration
To let docker use the memory stick, we have to set the base path to the stick's mount directory. To achieve this we create an drop-in configuration file.

Create the drop-in configuration file  ```/etc/systemd/system/docker.conf```

    [Service]
    ExecStart=/usr/bin/dockerd --graph="/mnt/pi/docker" --storage-driver=overlay2 -H fd://


## Check Modification
To verify the modification works, display running docker's information.

    sudo docker info

It should show the modified root directory.

    Storage Driver: overlay2
     Backing Filesystem: extfs
     Supports d_type: true
     Native Overlay Diff: true
    ...
    Docker Root Dir: /mnt/pi/docker
    ...

