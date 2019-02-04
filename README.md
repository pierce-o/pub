
# PUB - A USB Backup Software

<p align="center">
  <img src="https://i.imgur.com/KwlWnlQ.png">
</p>

# What is PUB?
PUB is a program designed to backup a USB drive once it is plugged in to a computer. It supports multiple drives to be backed up and allows for them to be backed up in multiple ways.

# Current Features

 - Adding and removing devices from a white-list.
 - Applying custom backup settings to individual devices.
 - Saving those white-listed devices and their settings.
 - Detecting when a new device has been inserted into the computer.
 - Event log from the programs processes.
 - Setting a backup location.
# Known Bugs
 - When the program is rebooted it sometimes removes the previous backup settings. Hasn't been looked into.
 - When a device is removed from the computer it doesn't remove it from the list. This isn't necessarily a bug but it is something that needs to be implemented using `DBT_DEVICEREMOVECOMPLETE`.

# Download
You can either manually download the solution and build it your self or get the latest download from the release page, [here](https://github.com/pierce-o/pub/releases).

# Preview and Usage
![enter image description here](https://imgur.com/5dVTPS7.png)

### File Resolver Method
This is what will determine if a USB drive needs to be backed up or not.

 1. High - Will back up all files no matter what.
 2. Medium - Will only back up files if they were last modified more than a day ago. 
 3. Low - Will back up all files if the total available space on the drive has changed.

### Archive Method
This is what will determine how files will be backed up.

 1. High - Will back up files in a new dated folder.
 2. Medium - Will only backup files if they have been changed or don't exist. 
 3. Low - Will only back up files if they have been modified over a day ago.


