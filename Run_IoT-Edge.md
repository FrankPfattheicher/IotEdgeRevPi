
## Run IoT-Edge
Due to the edgeAgent requires to listen on port 443 we have to stop and disable the apache server.

    sudo systemctl stop apache2
    sudo systemctl disable apache2
    
Check that there is no other program listening on 443.

    sudo netstat -pln

