using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private int iterations = 10;
    [SerializeField] public int walkLength = 10;
    [SerializeField] public bool startRandomlyEachIteration = true;

    
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPositions);
    }
    protected HashSet<Vector2Int>RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPostions = new HashSet<Vector2Int>();
        for(int I =0; I <iterations; I++)
        {
            var path = ProceduralGeneralAlgorithm.SimpleRandomWalk(currentPosition, walkLength);
            floorPostions.UnionWith(path);
            if(startRandomlyEachIteration)
            {
                currentPosition = floorPostions.ElementAt(Random.Range(0,floorPostions.Count));
            }
        }
        return floorPostions;
    }
}
