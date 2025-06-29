# [Hydroponics Growth Sync (Continued)](https://steamcommunity.com/sharedfiles/filedetails/?id=2018301976)

![Image](https://i.imgur.com/buuPQel.png)

    Update of ChippedChaps mod
    https://steamcommunity.com/sharedfiles/filedetails/?id=1537872509

- Fixed the issue with invisible plants

![Image](https://i.imgur.com/pufA0kM.png)
	
![Image](https://i.imgur.com/Z4GOv8H.png)

**Legacy 1.0 Version**
For now, please use the updated version by Jellypowered here:
https://steamcommunity.com/sharedfiles/filedetails/?id=2018301976
I plan to only make my own version if I add any new features - I'm grateful for any unofficial maintenance and I will only supersede unofficial updates until there is a good reason to do so.

# Changes the growth of plants in hydroponics so that all plants grow to maturity at the same time


The mod finds groups of connected, plant-growing buildings* and syncs all the plants that are grown in each group. For example, if you have a group of hydroponics tables that are next to each other, the plants in those hydroponics tables are synced. The plants in groups of hydroponics that are not adjacent to each other are synced separately.

* Any building that can grow plants, so this should be compatible with mods that add new hydroponics tables.

**The plant syncing code was heavily based on the plant syncing code in [Plant Growth Sync](https://steamcommunity.com/sharedfiles/filedetails/?id=1454228967) by Lanilor**

# Additional Notes:

This mod only affects plants grown in plant-growing buildings. Check out Plant Growth Sync if you want plants in growing zones to be synced.

Every in-game hour, the mod loops through all plant-growing buildings in the map and makes groups of connected plant-growing buildings that are growing the same plant. The mod then syncs all the plants grown by the plant-growing buildings in each group. This shouldn't slow down your game much because it only runs every in-game hour.

If you think there are any issues with how the mod identifies groups of buildings, open the debug menu and check the DrawGroups option in the TweakValues menu. A number will be drawn on all plant-growing buildings present on the map, with buildings of the same group having the same number. Use this to confirm if there are any issues with group-building.


![Image](https://i.imgur.com/PwoNOj4.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using [HugsLib](https://steamcommunity.com/workshop/filedetails/?id=818773962) or the standalone [Uploader](https://steamcommunity.com/sharedfiles/filedetails/?id=2873415404) and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.
-  Use [RimSort](https://github.com/RimSort/RimSort/releases/latest) to sort your mods

 

[![Image](https://img.shields.io/github/v/release/emipa606/HydroponicsGrowthSync?label=latest%20version&style=plastic&color=9f1111&labelColor=black)](https://steamcommunity.com/sharedfiles/filedetails/changelog/2018301976) | tags:  growth,  synchronization
