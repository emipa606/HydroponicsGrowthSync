using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace HydroponicsGrowthSync
{
    // Token: 0x02000002 RID: 2
    public class PlantGrowerGrowthSyncer : MapComponent
    {
        // Token: 0x04000002 RID: 2
        [TweakValue("PlantGrowerGrowthSyncer")]
        public static bool DrawGroups = false;

        // Token: 0x04000003 RID: 3
        [TweakValue("PlantGrowerGrowthSyncer", 0f, 2.2f)]
        public static float SyncRatePerFullGrowth = 1.15f;

        // Token: 0x04000001 RID: 1
        private readonly List<List<Thing>> plantGrowerGroups = new List<List<Thing>>();

        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public PlantGrowerGrowthSyncer(Map map) : base(map)
        {
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
        public override void MapComponentTick()
        {
            if (Find.TickManager.TicksGame % 2000 != 0)
            {
                return;
            }

            BuildPlantGrowerGroups();
            SyncAllGroups();
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
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

        // Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
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
                        var lhs = (Building_PlantGrower) thing;
                        foreach (var thing2 in enumerable)
                        {
                            var building_PlantGrower = (Building_PlantGrower) thing2;
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

        // Token: 0x06000005 RID: 5 RVA: 0x000022A4 File Offset: 0x000004A4
        private void SyncAllGroups()
        {
            foreach (var things in plantGrowerGroups)
            {
                var plantDefToGrow = (things[0] as Building_PlantGrower)?.GetPlantDefToGrow();
                var list = new List<Plant>();
                var num = 0f;
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
                        num += plant.Growth;
                    }
                }

                if (list.Count < 2)
                {
                    continue;
                }

                num /= list.Count;
                if (plantDefToGrow == null)
                {
                    continue;
                }

                var num2 = SyncRatePerFullGrowth / (plantDefToGrow.plant.growDays * 30f);
                var num3 = 0;
                var num4 = 0;
                var num5 = list.Count - 1;
                while (0 <= num5)
                {
                    if (Mathf.Abs(num - list[num5].Growth) <= num2)
                    {
                        list[num5].Growth = num;
                        list.RemoveAt(num5);
                    }
                    else
                    {
                        if (list[num5].Growth > num)
                        {
                            num4++;
                        }

                        if (list[num5].Growth < num)
                        {
                            num3++;
                        }
                    }

                    num5--;
                }

                var num6 = 1f;
                var num7 = 1f;
                if (num3 > 0 && num4 > 0)
                {
                    if (num3 > num4)
                    {
                        num6 = num4 / (float) num3;
                    }
                    else
                    {
                        num7 = num3 / (float) num4;
                    }
                }

                foreach (var plant in list)
                {
                    if (plant.Growth < num)
                    {
                        plant.Growth += num2 * num6;
                    }

                    if (plant.Growth > num)
                    {
                        plant.Growth -= num2 * num7;
                    }
                }
            }
        }
    }
}