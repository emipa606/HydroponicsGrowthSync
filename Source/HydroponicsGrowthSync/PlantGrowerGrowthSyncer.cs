using System;
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
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PlantGrowerGrowthSyncer(Map map) : base(map)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public override void MapComponentTick()
		{
			if (Find.TickManager.TicksGame % 2000 == 0)
			{
				this.BuildPlantGrowerGroups();
				this.SyncAllGroups();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
		public override void MapComponentOnGUI()
		{
			if (PlantGrowerGrowthSyncer.DrawGroups)
			{
				for (int i = 0; i < this.plantGrowerGroups.Count; i++)
				{
					for (int j = 0; j < this.plantGrowerGroups[i].Count; j++)
					{
						Vector2 vector = UI.MapToUIPosition(this.plantGrowerGroups[i][j].DrawPos);
						Rect rect = new Rect(vector.x, vector.y, 999f, 50f);
						Text.Font = GameFont.Medium;
						Widgets.Label(rect, i.ToString());
						Text.Font = GameFont.Small;
					}
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		private void BuildPlantGrowerGroups()
		{
			this.plantGrowerGroups.Clear();
			IEnumerable<Thing> enumerable = from x in this.map.listerThings.ThingsInGroup(ThingRequestGroup.BuildingArtificial)
			where x is Building_PlantGrower
			select x;
			HashSet<Thing> hashSet = new HashSet<Thing>();
			HashSet<Thing> hashSet2 = new HashSet<Thing>();
			foreach (Thing item in enumerable)
			{
				if (!hashSet.Contains(item))
				{
					List<Thing> list = new List<Thing>();
					hashSet2.Add(item);
					while (hashSet2.Count > 0)
					{
						hashSet.AddRange(hashSet2);
						list.AddRange(hashSet2);
						HashSet<Thing> hashSet3 = new HashSet<Thing>(hashSet2);
						hashSet2.Clear();
						foreach (Thing thing in hashSet3)
						{
							Building_PlantGrower lhs = (Building_PlantGrower)thing;
							foreach (Thing thing2 in enumerable)
							{
								Building_PlantGrower building_PlantGrower = (Building_PlantGrower)thing2;
								if (!hashSet.Contains(building_PlantGrower) && lhs.CanBeGroupedTo(building_PlantGrower))
								{
									hashSet2.Add(building_PlantGrower);
								}
							}
						}
					}
					this.plantGrowerGroups.Add(list);
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022A4 File Offset: 0x000004A4
		private void SyncAllGroups()
		{
			for (int i = 0; i < this.plantGrowerGroups.Count; i++)
			{
				ThingDef plantDefToGrow = (this.plantGrowerGroups[i][0] as Building_PlantGrower).GetPlantDefToGrow();
				List<Plant> list = new List<Plant>();
				float num = 0f;
				for (int j = 0; j < this.plantGrowerGroups[i].Count; j++)
				{
					foreach (Plant plant in (this.plantGrowerGroups[i][j] as Building_PlantGrower).PlantsOnMe)
					{
						if (plant.def == plantDefToGrow && plant.IsCrop && plant.LifeStage == PlantLifeStage.Growing)
						{
							list.Add(plant);
							num += plant.Growth;
						}
					}
				}
				if (list.Count >= 2)
				{
					num /= (float)list.Count;
					float num2 = PlantGrowerGrowthSyncer.SyncRatePerFullGrowth / (plantDefToGrow.plant.growDays * 30f);
					int num3 = 0;
					int num4 = 0;
					int num5 = list.Count - 1;
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
					float num6 = 1f;
					float num7 = 1f;
					if (num3 > 0 && num4 > 0)
					{
						if (num3 > num4)
						{
							num6 = (float)num4 / (float)num3;
						}
						else
						{
							num7 = (float)num3 / (float)num4;
						}
					}
					for (int k = 0; k < list.Count; k++)
					{
						if (list[k].Growth < num)
						{
							list[k].Growth += num2 * num6;
						}
						if (list[k].Growth > num)
						{
							list[k].Growth -= num2 * num7;
						}
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private List<List<Thing>> plantGrowerGroups = new List<List<Thing>>();

		// Token: 0x04000002 RID: 2
		[TweakValue("PlantGrowerGrowthSyncer", 0f, 100f)]
		public static bool DrawGroups = false;

		// Token: 0x04000003 RID: 3
		[TweakValue("PlantGrowerGrowthSyncer", 0f, 2.2f)]
		public static float SyncRatePerFullGrowth = 1.15f;
	}
}
