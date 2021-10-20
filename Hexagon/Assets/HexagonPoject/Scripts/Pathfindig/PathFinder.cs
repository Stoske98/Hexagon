using PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinder : MonoBehaviour
{
    public static List<Hex> FindPath_AStar(Hex start, Hex end)
    {

        foreach (var tile in Map.Instance.map)
        {

            tile.Cost = int.MaxValue;
        }

        start.Cost = 0;
        Comparison<Hex> heuristicComparison = (lhs, rhs) =>
        {
            float lhsCost = lhs.Cost + GetEuclideanHeuristicCost(lhs, end);
            float rhsCost = rhs.Cost + GetEuclideanHeuristicCost(rhs, end);

            return lhsCost.CompareTo(rhsCost);
        };
        MinHeap<Hex> frontier = new MinHeap<Hex>(heuristicComparison);
        frontier.Add(start);

        HashSet<Hex> visited = new HashSet<Hex>();
        visited.Add(start);

        start.PrevTile = null;

        while (frontier.Count > 0)
        {
            Hex current = frontier.Remove();

            if (current == end)
            {
                break;
            }

            foreach (var neighbor in current.neighbors)
            {

                if (neighbor.Walkable)
                {

                    int newNeighborCost = current.Cost + neighbor.Weight;
                    if (newNeighborCost < neighbor.Cost)
                    {
                        neighbor.Cost = newNeighborCost;
                        neighbor.PrevTile = current;

                    }

                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        frontier.Add(neighbor);


                    }
                }
            }
        }
        List<Hex> path = BacktrackToPath(end);
        return path;
    }

    public static bool InRange(Hex start, Hex end, int range)
    {
        foreach (var tile in Map.Instance.map)
        {

            tile.Cost = int.MaxValue;
        }

        start.Cost = 0;
        Comparison<Hex> heuristicComparison = (lhs, rhs) =>
        {
            float lhsCost = lhs.Cost + GetEuclideanHeuristicCost(lhs, end);
            float rhsCost = rhs.Cost + GetEuclideanHeuristicCost(rhs, end);

            return lhsCost.CompareTo(rhsCost);
        };
        MinHeap<Hex> frontier = new MinHeap<Hex>(heuristicComparison);
        frontier.Add(start);

        HashSet<Hex> visited = new HashSet<Hex>();
        visited.Add(start);

        start.PrevTile = null;

        while (frontier.Count > 0)
        {
            Hex current = frontier.Remove();
            if (current == end)
            {

                break;
            }

            foreach (var neighbor in current.neighbors)
            {
                int newNeighborCost = current.Cost + neighbor.Weight;
                if (newNeighborCost < neighbor.Cost)
                {
                    neighbor.Cost = newNeighborCost;
                    neighbor.PrevTile = current;
                }

                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    frontier.Add(neighbor);
                }

            }
        }
        return end.Cost <= range;
    }

    public static List<Hex> BFS_ListInRange(Hex start, float MoveRange)
    {
        List<Hex> tilesInRange = new List<Hex>();

        foreach (var tile in Map.Instance.map)
        {
            tile.Cost = int.MaxValue;
        }

        start.Cost = 0;

        HashSet<Hex> visited = new HashSet<Hex>();
        visited.Add(start);

        Queue<Hex> frontier = new Queue<Hex>();
        frontier.Enqueue(start);

        start.PrevTile = null;

        while (frontier.Count > 0)
        {
            Hex current = frontier.Dequeue();


            foreach (var neighbor in current.neighbors)
            {
                if (neighbor.Walkable)
                {
                    int newNeighborCost = current.Cost + neighbor.Weight;

                    if (newNeighborCost < neighbor.Cost)
                    {
                        neighbor.Cost = newNeighborCost;
                        neighbor.PrevTile = current;
                    }


                    if (!visited.Contains(neighbor))
                    {
                        if (MoveRange - newNeighborCost >= 0)
                        {
                            tilesInRange.Add(neighbor);
                        }
                        else
                        {
                            return tilesInRange;
                        }
                        visited.Add(neighbor);
                        frontier.Enqueue(neighbor);
                    }
                }
            }
        }
        return tilesInRange;
    }


    private static float GetEuclideanHeuristicCost(Hex current, Hex end)
    {
        float heuristicCost = Mathf.Pow(end.Column - current.Column, 2) + Mathf.Pow(end.Row - current.Row, 2);
        return heuristicCost;
    }

    private static List<Hex> BacktrackToPath(Hex end)
    {
        Hex current = end;
        List<Hex> path = new List<Hex>();
        while (current != null)
        {

            path.Add(current);
            current = current.PrevTile;
        }

        path.Reverse();


        return path;
    }
}
