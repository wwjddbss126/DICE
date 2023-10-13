# DICE
![image](https://github.com/wwjddbss126/DICE/assets/49504937/ff2faaa3-4e15-48a7-940b-b18fb1e17a3a)

Digital forensic Inspector for Cloud storage Evidence - MEGA Cloud
---
DICE-M(Digital forensic Inspector for Cloud storage Evidence - MEGA Cloud) is a graphical user interface (GUI) toolkit developed using Devexpress WPF and C#. It provides two main functionalities: exploration and collection of data within MEGA Cloud using user credentials, and the ability to perform a Credential Cloning Attack using Windows memory dump files on Mega Cloud.

## Table of Contents
- [Acknowledgements](#acknowledgements)
- [Features](#features)
- [Requirements](#requirements)
- [Usage](#usage)
- [License](#license)

## Acknowledgements

DICE-M is built upon the codebase originally created by [Grégoire Pailler](https://github.com/gpailler/MegaApiClient) and is released under the MIT License. 
We are grateful for the inspiration their work has provided to our software development and appreciate the fact that their use of the MIT License has allowed us to freely extend and build upon their contributions without any legal restrictions.

## Features

DICE-M is a tool that can be used for digital forensic analysis of MEGA Cloud, a personal cloud storage service with end-to-end encryption.
It offers the following features:
1. **MEGA Cloud Data Exploration and Collection**: Use your MEGA Cloud login credentials(ID/PW) to navigate and collect data within the MEGA Cloud storage.

2. **Credential Cloning Attack**: Utilize memory dump files to execute a Credential Cloning Attack on MEGA Cloud. A memory dump on a PC logged into the MEGA Cloud with a web browser allows to explore crime-related storage without the need for proper ID and PW.

## Requirements

1. DevExpress v22.1 
2. Memory dump created by Windows OS that logged into the MEGA Cloud using a web browser

## Usage 

1. Launch DICE-M (DICE/Release/DICE.Main.exe).

2. Choose the desired operation (A. Data Exploration or B. Credential Cloning Attack).
![image](https://github.com/wwjddbss126/DICE/assets/49504937/17229024-c7b6-43e6-a6ec-806c07c7a2ec)

3-A. Authenticate users by entering a valid ID, PW of MEGA Cloud storage that requires digital forensic investigation. 

3-B. Enter the PC memory dump file. If the login-related data contained in the memory file is valid, it will be connected to cloud storage through the Browser Automation Program.

4-A. Explore cloud storage based on collected metadata and collect files related to crime.

4-B. Explore cloud storage connected to web browser. **Be careful not to delete/modify at this time and use methods such as leaving video records.**

**Please note**: The Credential Cloning Attack feature should be used for criminal investigation and evidence collection purposes only. Unauthorized access or modification to the system is illegal and unethical.

## License

DICE-M is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

**Disclaimer**: This software is further developed based on code implemented by Grégoire Pailer(https://github.com/gpailler/MegaApiClient.git) and is available for digital forensic investigation purposes.The developers are not responsible for any misuse or illegal activities conducted with this software. Use this software responsibly and in compliance with all applicable laws and regulations.
