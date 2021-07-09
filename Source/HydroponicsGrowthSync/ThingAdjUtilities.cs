using RimWorld;
using Verse;

namespace HydroponicsGrowthSync
{
    // Token: 0x02000003 RID: 3
    internal static class ThingAdjUtilities
    {
        // Token: 0x06000007 RID: 7 RVA: 0x000024F2 File Offset: 0x000006F2
        public static bool CanBeGroupedTo(this Building_PlantGrower lhs, Building_PlantGrower rhs)
        {
            return lhs.IsConnectedTo(rhs) && lhs.GetPlantDefToGrow() == rhs.GetPlantDefToGrow();
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002510 File Offset: 0x00000710
        public static bool IsConnectedTo(this Thing lhs, Thing rhs)
        {
            var array = lhs.AdjacencyRects();
            foreach (var c in rhs.OccupiedRect())
            {
                for (var i = 0; i < 2; i++)
                {
                    if (array[i].Contains(c))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Token: 0x06000009 RID: 9 RVA: 0x0000258C File Offset: 0x0000078C
        public static CellRect[] AdjacencyRects(this Thing thing)
        {
            var array = new CellRect[2];
            var cellRect = thing.OccupiedRect();
            array[0] = new CellRect(cellRect.minX - 1, cellRect.minZ, cellRect.Width + 2, cellRect.Height);
            array[1] = new CellRect(cellRect.minX, cellRect.minZ - 1, cellRect.Width, cellRect.Height + 2);
            return array;
        }
    }
}