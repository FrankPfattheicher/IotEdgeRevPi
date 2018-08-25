
## Prepare Installation

### Purge Not Used Programs
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

