using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace HydroponicsGrowthSync;

public class PlantGrowerGrowthSyncer : MapComponent
{
    [TweakValue("PlantGrowerGrowthSyncer")]
    public static bool DrawGroups = false;

    [TweakValue("PlantGrowerGrowthSyncer", 0f, 2.2f)]
    public static float SyncRatePerFullGrowth = 1.15f;

    private readonly List<List<Thing>> plantGrowerGroups = new List<List<Thing>>();

    public PlantGrowerGrowthSyncer(Map map) : base(map)
    {
    }

    public override void MapComponentTick()
    {
        if (Find.TickManager.TicksGame % 2000 != 0)
        {
            return;
        }

        BuildPlantGrowerGroups();
        SyncAllGroups();
    }

    public override void MapComponentOnGUI()
    {
        if (!DrawGroups)
        {
            return;
        }

        for (var i = 0; i < plantGrowerGroups.Count; i++)
        {
            for (var j = 0; j < plantGrowerGroups[i].Count; j++)
            {
                var vector = plantGrowerGroups[i][j].DrawPos.MapToUIPosition();
                var rect = new Rect(vector.x, vector.y, 999f, 50f);
                Text.Font = GameFont.Medium;
                Widgets.Label(rect, i.ToString());
                Text.Font = GameFont.Small;
            }
        }
    }

    private void BuildPlantGrowerGroups()
    {
        plantGrowerGroups.Clear();
        var enumerable = from x in map.listerThings.ThingsInGroup(ThingRequestGroup.BuildingArtificial)
            where x is Building_PlantGrower
            select x;
        var hashSet = new HashSet<Thing>();
        var hashSet2 = new HashSet<Thing>();
        foreach (var item in enumerable)
        {
            if (hashSet.Contains(item))
            {
                continue;
            }

            var list = new List<Thing>();
            hashSet2.Add(item);
            while (hashSet2.Count > 0)
            {
                hashSet.AddRange(hashSet2);
                list.AddRange(hashSet2);
                var hashSet3 = new HashSet<Thing>(hashSet2);
                hashSet2.Clear();
                foreach (var thing in hashSet3)
                {
                    var lhs = (Building_PlantGrower)thing;
                    foreach (var thing2 in enumerable)
                    {
                        var building_PlantGrower = (Building_PlantGrower)thing2;
                        if (!hashSet.Contains(building_PlantGrower) && lhs.CanBeGroupedTo(building_PlantGrower))
                        {
                            hashSet2.Add(building_PlantGrower);
                        }
                    }
                }
            }

            plantGrowerGroups.Add(list);
        }
    }

    private void SyncAllGroups()
    {
        foreach (var things in plantGrowerGroups)
        {
            var plantDefToGrow = (things[0] as Building_PlantGrower)?.GetPlantDefToGrow();
            var list = new List<Plant>();
            var averagePlantGrowth = 0f;
            foreach (var thing in things)
            {
                var plantsOnMe = (thing as Building_PlantGrower)?.PlantsOnMe;
                if (plantsOnMe == null)
                {
                    continue;
                }

                foreach (var plant in plantsOnMe)
                {
                    if (plant.def != plantDefToGrow || !plant.IsCrop || plant.LifeStage != PlantLifeStage.Growing)
                    {
                        continue;
                    }

                    list.Add(plant);
                    averagePlantGrowth += plant.Growth;
                }
            }

            if (list.Count < 2)
            {
                continue;
            }

            averagePlantGrowth /= list.Count;
            if (plantDefToGrow == null)
            {
                continue;
            }

            var growthRate = SyncRatePerFullGrowth / (plantDefToGrow.plant.growDays * 30f);
            var lowGrowthPlants = 0;
            var highGrowthPlants = 0;
            var plantArrayCount = list.Count - 1;
            while (0 <= plantArrayCount)
            {
                if (Mathf.Abs(averagePlantGrowth - list[plantArrayCount].Growth) <= growthRate)
                {
                    list[plantArrayCount].Growth = averagePlantGrowth;
                    list.RemoveAt(plantArrayCount);
                }
                else
                {
                    if (list[plantArrayCount].Growth > averagePlantGrowth)
                    {
                        highGrowthPlants++;
                    }

                    if (list[plantArrayCount].Growth < averagePlantGrowth)
                    {
                        lowGrowthPlants++;
                    }
                }

                plantArrayCount--;
            }

            var highGrowthRate = 1f;
            var lowGrowthRate = 1f;
            if (lowGrowthPlants > 0 && highGrowthPlants > 0)
            {
                if (lowGrowthPlants > highGrowthPlants)
                {
                    highGrowthRate = highGrowthPlants / (float)lowGrowthPlants;
                }
                else
                {
                    lowGrowthRate = lowGrowthPlants / (float)highGrowthPlants;
                }
            }

            foreach (var plant in list)
            {
                if (plant.Growth < averagePlantGrowth)
                {
                    plant.Growth += growthRate * highGrowthRate;
                }

                if (!(plant.Growth > averagePlantGrowth))
                {
                    continue;
                }

                if (plant.Growth - (growthRate * lowGrowthRate) < averagePlantGrowth)
                {
                    plant.Growth = averagePlantGrowth;
                    continue;
                }

                plant.Growth -= growthRate * lowGrowthRate;
            }
        }
    }
}