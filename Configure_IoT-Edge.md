
## Configure IoT-Edge
After modifying docker installation to use the USB stick to store images it took a bit longer for docker to start up. Microsoft is missing to add the docker service as dependency for the iotedge service. So we have to add this.

> After=network-online.target iotedge.socket iotedge.mgmt.socket **docker.service**    
> Requires=network-online.target iotedge.socket iotedge.mgmt.socket **docker.service**

To be save i additionally added a 60 second delay before starting iotedge.

> **ExecStartPre=/bin/sleep 60**

The configuration file ```/lib/systemd/system/iotedge.service``` now looks like this:

    [Unit]
    Description=Azure IoT Edge daemon
    After=network-online.target iotedge.socket iotedge.mgmt.socket docker.service
    Requires=network-online.target iotedge.socket iotedge.mgmt.socket docker.service
    Documentation=man:iotedged(8)

    [Service]
    ExecStartPre=/bin/sleep 60
    ExecStart=/usr/bin/iotedged -c /etc/iotedge/config.yaml
    KillMode=process
    TimeoutStartSec=600
    TimeoutStopSec=40
    Restart=on-failure
    User=iotedge
    Group=iotedge

    [Install]
    WantedBy=multi-user.target
    Also=iotedge.socket iotedge.mgmt.socket

