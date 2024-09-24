using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ProceduralGeneralAlgorithm 
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousposition = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousposition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousposition = newPosition;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corriderLength)
    {
        List<Vector2Int> corridor  =new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);
        for(int i=0; i<corriderLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
    public static List<BoundsInt>BinarySpacePartitioning(BoundsInt spaceToSplit, int minwidth,int minheight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();  
        roomsQueue.Enqueue(spaceToSplit);  //데이터 입력 
        while(roomsQueue.Count > 0)
        {
            var room= roomsQueue.Dequeue(); //데이터 출력
            if(room.size.y>=minheight&&room.size.x>=minwidth)
            {
                if(Random.value<0.5f)
                {
                    if(room.size.y>=minheight*2)
                    {
                        SplitHorizontally( minheight, roomsQueue, room);
                    }
                    else if(room.size.x>=minwidth*2)
                    {
                        SplitVertically( minheight, roomsQueue, room);
                    }
                    else if(room.size.x>=minwidth&&room.size.y>=minheight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.y >= minheight * 2)
                    {
                        SplitHorizontally(minwidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minwidth * 2)
                    {
                        SplitVertically(minheight, roomsQueue, room);
                    }
                    else if (room.size.x >= minwidth && room.size.y >= minheight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minwidth,  Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x); //1~room.size.x-1까지 
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.min.y, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minheight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y); //minHeight, room.size.y-minHeight
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x,room.min.y+ySplit,room.min.z),
            new Vector3Int(room.size.x,room.size.y-ySplit,room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // up
        new Vector2Int(1,0), //Right 
        new Vector2Int(0,-1), // down
        new Vector2Int(-1,0) // LEFT
    };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];
    }
}