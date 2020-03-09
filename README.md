# HydroponicsGrowthSync

Update of ChippedChaps mod for RimWorld 1.1
    https://steamcommunity.com/sharedfiles/filedetails/?id=1537872509

    Support-chat:
    https://discord.gg/SuhwVpM

    Non-steam version:
    https://github.com/emipa606/HydroponicsGrowthSync

    --- Original Description ---
Syncs plant growth such that all plants growing in a group of connected hydroponics tables growing the same plant will reach maturity at roughly the same time.

The plant syncing code used was heavily based on the plant syncing code of\n&lt;b&gt;Plant Growth Sync by Lanilor&lt;/b&gt;

&lt;b&gt;Additional Notes:&lt;/b&gt;
This mod only affects plants grown in plant-growing buildings. Check out Plant Growth Sync if you want plants in growing zones to be synced.

Every in-game hour, the mod loops through all plant-growing buildings in the map and makes groups of connected plant-growing buildings that are growing the same plant. The mod then syncs all the plants grown by the plant-growing buildings in each group. This shouldn't slow down your game much because it only runs every in-game hour.

If you think there are any issues with how the mod identifies groups of buildings, open the debug menu and check the DrawGroups option in the TweakValues menu. A number will be drawn on all plant-growing buildings present on the map, with buildings of the same group having the same number. Use this to confirm if there are any issues with group-building.
