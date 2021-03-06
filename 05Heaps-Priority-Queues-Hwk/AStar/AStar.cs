﻿using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] maze;

    public AStar(char[,] maze)
    {
        this.maze = maze;
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaY + deltaX;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var pQueue = new PriorityQueue<Node>();
        var parent = new Dictionary<Node, Node>();
        var cost = new Dictionary<Node, int>();

        pQueue.Enqueue(start);
        parent[start] = null;
        cost[start] = 0;

        while (pQueue.Count > 0)
        {
            var current = pQueue.Dequeue();
            if (current.Equals(goal))
            {
                break;
            }

            var neighbors = this.GetNeighbors(current);

            var newCost = cost[current] + 1;
            foreach (var neighbor in neighbors)
            {
                if (!cost.ContainsKey(neighbor) || newCost < cost[neighbor])
                {
                    neighbor.F = newCost + GetH(neighbor, goal);
                    parent[neighbor] = current;
                    cost[neighbor] = newCost;
                    pQueue.Enqueue(neighbor);
                }
            }

        }

        return this.ReconstructPath(start, goal, parent);

    }

    private IEnumerable<Node> ReconstructPath(Node start, Node goal, Dictionary<Node, Node> parent)
    {
        var current = goal;
        var path = new Stack<Node>();
        if (!parent.ContainsKey(goal))
        {
            path.Push(start);
            return path;
        }

        while (current != null)
        {
            path.Push(current);
            current = parent[current];
            if (current == null)
            {
                break;
            }
        }

        return path;
    }

    private List<Node> GetNeighbors(Node current)
    {
        var row = current.Row;
        var col = current.Col;

        var rowUp = row - 1;
        var rowDown = row + 1;
        var colLeft = col - 1;
        var colRight = col + 1;

        var result = new List<Node>();
        this.AddToQueue(result, rowUp, col);
        this.AddToQueue(result, rowDown, col);
        this.AddToQueue(result, row, colLeft);
        this.AddToQueue(result, row, colRight);

        return result;
    }

    private void AddToQueue(List<Node> list, int row, int col)
    {
        if (this.IsInBound(row, col) && !this.IsWall(row, col))
        {
            var newNode = new Node(row, col);
            list.Add(newNode);
        }
    }

    private bool IsInBound(int row, int col)
    {
        return (row >= 0 && row < this.maze.GetLength(0))
               && (col >= 0 && col < this.maze.GetLength(1));
    }

    private bool IsWall(int row, int col)
    {
        return this.maze[row, col] == 'W';
    }

}

