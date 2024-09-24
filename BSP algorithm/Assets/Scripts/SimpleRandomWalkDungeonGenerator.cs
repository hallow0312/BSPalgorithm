using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkSO randomwalkParameter;

    
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomwalkParameter,startPosition);
        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }
    protected HashSet<Vector2Int>RunRandomWalk(SimpleRandomWalkSO parameters,Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPostions = new HashSet<Vector2Int>();
        for(int I =0; I <parameters.iterations; I++)
        {
            var path = ProceduralGeneralAlgorithm.SimpleRandomWalk(currentPosition,parameters.walkLength);
            floorPostions.UnionWith(path);
            if(parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPostions.ElementAt(Random.Range(0,floorPostions.Count));
            }
        }
        return floorPostions;
    }
}
