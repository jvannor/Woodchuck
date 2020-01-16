# Woodchuck
Woodchuck is a simple home automation project that augments a Honeywell Wireless Doorbell by adding SMS/MMS text messaging capabilities.  The idea is to send a text mesage to a registered list of users whenever the Honeywell doorbell is pressed.  

See https://www.honeywellstore.com/store/products/wireless-portable-doorbell-with-halo-light-9-series-rdwl917ax.htm for more information

Woodchuck is comprised of two, small computing devices: a NodeMCU, equipped with a light sensor; and a Raspberry PI 3 Model B, equipped with the Raspbian OS, Docker, and Docker-Compose.

Woodchuck operates as follows:
1. The NodeMCU and its light sensor monitor the Honeywell Doorbell unit which flashes whenever the doorbell button is pressed.
2. In response to the light flash, the NodeMCU sends a HTTP request (POST) to a well-known URL, hosted on the Raspberry PI.
3. The post is received by an ASP.NET Web API project running in a Docker container on the Raspberry PI called, "MonitorAPI."
4. MonitorAPI responds to the post by enqueing a message with RabbitMQ, also running in a Docker container on the Raspberry PI.
5. A separate .NET Core Worker Service automatically receives that MonitorAPI message from RabbitMQ on the Raspberry PI.  This service is simply called, "WorkerService."  It is also running in a Docker container.
6. In resposne to the notification, WorkerService retrieves security camera images for a set of pre-configured URIs (configuration data is stored in a PostgreSQL database running as a Docker container on the Raspberry PI).
7. Once the images are retrieved, they are re-sized and uploaded to a cloud storage account
8. Once the images are uploaded and available, an API call is made to a third party SMS/MMS service to send text messages to the enrolled users
9. The system can be dynamically configured by a custom ASP.NET Web Application, called, "AdminWeb," that is also running in a Docker container on the Raspberry Pi.
