# TODO LIST (UPDATED EVERY COMMITS)
-------------------------
**AuthServer**
-----
* **Connect with server ID**
* World -> Auth internal server with server status update

**WorldServer**
-----
* **CharacterRecord**
  > Translate CharacterBaseInformation into database
  > Translate CharacterBaseInformation from database
  > Handle EntityLook from breeds with baseColor
* **Handle Character actions (remove, create)**
  > 160 <-- Character Creation 
  > 165 <-- Character Remove
  > 150 <-- Character List
* **Handle Character Base Informations**
  > CharacterBaseInformation --> 
  > ID, Level, Name, EntityLook (see CharacterRecord form translating), Breed, sex(Y/N)
* **Handle Character Stats List** 
  > HUGE message with all the character stats including all the stats of the character
* **Handle Character Inventory**
* **MapRecord**
* **Handle MapComplementaryInformations**
  > ID & Subarea only for now