using RimWorld;
using Verse;

namespace HydroponicsGrowthSync;

internal static class ThingAdjUtilities
{
    public static bool CanBeGroupedTo(this Building_PlantGrower lhs, Building_PlantGrower rhs)
    {
        return lhs.IsConnectedTo(rhs) && lhs.GetPlantDefToGrow() == rhs.GetPlantDefToGrow();
    }

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

    public static CellRect[] AdjacencyRects(this Thing thing)
    {
        var array = new CellRect[2];
        var cellRect = thing.OccupiedRect();
        array[0] = new CellRect(cellRect.minX - 1, cellRect.minZ, cellRect.Width + 2, cellRect.Height);
        array[1] = new CellRect(cellRect.minX, cellRect.minZ - 1, cellRect.Width, cellRect.Height + 2);
        return array;
    }
}