# ChangeLogs (0.3 updated)
**0.0.3 TestUpdate:**
-----
Almost all of that changes are made for test and can be undones on next updates. 
Almost every packets/functionalities are written in raw there.

* **NEW CLIENT LAUNCHER FOR SOUND**
* **A few changes on the Log format**
  Still no output logs but the server console is more "readable"
* **Jiva World Server added**
* **Handling Character creation**
  Only one character for all. Everytime a character is created, the main char is redefined
* **Handling Character Remove**
  Remove that only one character
* **Handling Character List**
  Once again, only one char
* **Handling World connexion**
  Empty inventory
  0 in all stats, 12AP 6MP
  Default map: Astrub Zaap
  EMPTY INVENTORY
  I messed up in something, you can't see the character name ingame on top on his model (mouseOver)
* **Handling local mouvements**
  You can move around in your client, nothing is sent to other clients though
* **Handling ADMIN TP**
  Double click on a pos in the Geo to TP into this map

**0.0.2:**
-----
* **Rework of user handling to a Threads system (One thread per user)**
* **Rework of packet handlers organization**
* **Removing old packet handling system**
* **Adding Nickname support (banned words only editable inside the code for now)**
* **Adding 'Find a friend' support (Search a friend servers in the server list)**
* **Adding a AuthServer commands handler (for admin console commands)**
* **Updating AccountManager**
* **Adding empty SQL database**

**0.0.1**
-----
* **Handle connection**
* **Handle identification**
* **Handle database server list**
* **Handle invalid packet error**
* **Handle character numbers for each servers from* database**
* **Handling subscribe time**
* **Handling banned time**
* **Handling admin rights**
