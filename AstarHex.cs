using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Astar3coordsImportantMethods
{
	public int distance(int sourceX, int sourceY, int sourceZ, int objectX, int objectY, int objectZ)
	{
		
		return (Math.Abs(sourceX - objectX) + Math.Abs(sourceY - objectY) + Math.Abs(sourceZ - objectZ)) / 2;

	}
	public List<AstarHex> neighbors(int sourceX, int sourceY, int sourceZ, int objectX, int objectY, int objectZ, int currentStep, int width, int height, Grid grid)
    {
		List<AstarHex> neighborsClass = new List<AstarHex>(); //0-x, 1-y, 2-z

		UnityEngine.Debug.Log(sourceX + ", " + sourceY + ", " + sourceZ);
		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX, yCoordinate = (sourceY - 1), zCoordinate = (sourceZ - 1), currentSteps = currentStep + 1, leastStepsToGoal = distance(sourceX, sourceY - 1, sourceZ - 1, objectX, objectY, objectZ) });
		if (neighborsClass[0].xCoordinate < 0 - (neighborsClass[0].yCoordinate / 2) || neighborsClass[0].xCoordinate > width - (neighborsClass[0].yCoordinate / 2) || neighborsClass[0].yCoordinate < 0 || neighborsClass[0].yCoordinate > height || neighborsClass[0].zCoordinate < 0 || neighborsClass[0].zCoordinate >= 13) { neighborsClass[0].betretbar = false; }
		UnityEngine.Debug.Log(neighborsClass[0].xCoordinate + ", " + neighborsClass[0].yCoordinate + ", " + neighborsClass[0].zCoordinate + ", " + neighborsClass[0].currentSteps + ", " + neighborsClass[0].leastStepsToGoal);
		for (int i = 0; i < grid.HexList.Count; i++)
		{

			if (grid.HexList[i].xCoordinate == neighborsClass[0].xCoordinate && grid.HexList[i].yCoordinate == neighborsClass[0].yCoordinate && grid.HexList[i].zCoordinate == neighborsClass[0].zCoordinate)
			{

				if (grid.HexList[i].betretbar == false) { neighborsClass[0].betretbar = grid.HexList[i].betretbar; }
				break;

			}

		}

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX + 1, yCoordinate = sourceY - 1, zCoordinate = sourceZ, currentSteps = currentStep + 1, leastStepsToGoal = distance(sourceX + 1, sourceY - 1, sourceZ, objectX, objectY, objectZ) });
		if (neighborsClass[1].xCoordinate < 0 - (neighborsClass[1].yCoordinate / 2) || neighborsClass[1].xCoordinate > width - (neighborsClass[1].yCoordinate / 2) || neighborsClass[1].yCoordinate < 0 || neighborsClass[1].yCoordinate > height || neighborsClass[1].zCoordinate < 0 || neighborsClass[1].zCoordinate >= 13) { neighborsClass[1].betretbar = false; }
		UnityEngine.Debug.Log(neighborsClass[1].xCoordinate + ", " + neighborsClass[1].yCoordinate + ", " + neighborsClass[1].zCoordinate + ", " + neighborsClass[1].currentSteps + ", " + neighborsClass[1].leastStepsToGoal);
		for (int i = 0; i < grid.HexList.Count; i++)
		{

			if (grid.HexList[i].xCoordinate == neighborsClass[1].xCoordinate && grid.HexList[i].yCoordinate == neighborsClass[1].yCoordinate && grid.HexList[i].zCoordinate == neighborsClass[1].zCoordinate)
			{

				if (grid.HexList[i].betretbar == false) { neighborsClass[1].betretbar = grid.HexList[i].betretbar; }
				break;

			}

		}

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX + 1, yCoordinate = sourceY, zCoordinate = sourceZ + 1, currentSteps = currentStep + 1, leastStepsToGoal = distance(sourceX + 1, sourceY, sourceZ + 1, objectX, objectY, objectZ) });
		if (neighborsClass[2].xCoordinate < 0 - (neighborsClass[2].yCoordinate / 2) || neighborsClass[2].xCoordinate > width - (neighborsClass[2].yCoordinate / 2) || neighborsClass[2].yCoordinate < 0 || neighborsClass[2].yCoordinate > height || neighborsClass[2].zCoordinate < 0 || neighborsClass[2].zCoordinate >= 13) { neighborsClass[2].betretbar = false; }
		UnityEngine.Debug.Log(neighborsClass[2].xCoordinate + ", " + neighborsClass[2].yCoordinate + ", " + neighborsClass[2].zCoordinate + ", " + neighborsClass[2].currentSteps + ", " + neighborsClass[2].leastStepsToGoal);
		for (int i = 0; i < grid.HexList.Count; i++)
		{

			if (grid.HexList[i].xCoordinate == neighborsClass[2].xCoordinate && grid.HexList[i].yCoordinate == neighborsClass[2].yCoordinate && grid.HexList[i].zCoordinate == neighborsClass[2].zCoordinate)
			{

				if (grid.HexList[i].betretbar == false) { neighborsClass[2].betretbar = grid.HexList[i].betretbar; }
				break;

			}

		}

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX, yCoordinate = sourceY + 1, zCoordinate = sourceZ + 1, currentSteps = currentStep + 1, leastStepsToGoal = distance(sourceX, sourceY + 1, sourceZ + 1, objectX, objectY, objectZ) });
		if (neighborsClass[3].xCoordinate < 0 - (neighborsClass[3].yCoordinate / 2) || neighborsClass[3].xCoordinate > width - (neighborsClass[3].yCoordinate / 2) || neighborsClass[3].yCoordinate < 0 || neighborsClass[3].yCoordinate > height || neighborsClass[3].zCoordinate < 0 || neighborsClass[3].zCoordinate >= 13) { neighborsClass[3].betretbar = false; }
		UnityEngine.Debug.Log(neighborsClass[3].xCoordinate + ", " + neighborsClass[3].yCoordinate + ", " + neighborsClass[3].zCoordinate + ", " + neighborsClass[3].currentSteps + ", " + neighborsClass[3].leastStepsToGoal);
		for (int i = 0; i < grid.HexList.Count; i++)
		{

			if (grid.HexList[i].xCoordinate == neighborsClass[3].xCoordinate && grid.HexList[i].yCoordinate == neighborsClass[3].yCoordinate && grid.HexList[i].zCoordinate == neighborsClass[3].zCoordinate)
			{

				if (grid.HexList[i].betretbar == false) { neighborsClass[3].betretbar = grid.HexList[i].betretbar; }
				break;

			}

		}

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX - 1, yCoordinate = sourceY + 1, zCoordinate = sourceZ, currentSteps = currentStep + 1, leastStepsToGoal = distance(sourceX - 1, sourceY + 1, sourceZ, objectX, objectY, objectZ) });
		if (neighborsClass[4].xCoordinate < 0 - (neighborsClass[4].yCoordinate / 2) || neighborsClass[4].xCoordinate > width - (neighborsClass[4].yCoordinate / 2) || neighborsClass[4].yCoordinate < 0 || neighborsClass[4].yCoordinate > height || neighborsClass[4].zCoordinate < 0 || neighborsClass[4].zCoordinate >= 13) { neighborsClass[4].betretbar = false; }
		UnityEngine.Debug.Log(neighborsClass[4].xCoordinate + ", " + neighborsClass[4].yCoordinate + ", " + neighborsClass[4].zCoordinate + ", " + neighborsClass[4].currentSteps + ", " + neighborsClass[4].leastStepsToGoal);
		for (int i = 0; i < grid.HexList.Count; i++)
		{

			if (grid.HexList[i].xCoordinate == neighborsClass[4].xCoordinate && grid.HexList[i].yCoordinate == neighborsClass[4].yCoordinate && grid.HexList[i].zCoordinate == neighborsClass[4].zCoordinate)
			{

				if (grid.HexList[i].betretbar == false) { neighborsClass[4].betretbar = grid.HexList[i].betretbar; }
				break;

			}

		}

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX - 1, yCoordinate = sourceY, zCoordinate = sourceZ - 1, currentSteps = currentStep + 1, leastStepsToGoal = distance(sourceX - 1, sourceY, sourceZ - 1, objectX, objectY, objectZ) });
		if (neighborsClass[5].xCoordinate < 0 - (neighborsClass[5].yCoordinate / 2) || neighborsClass[5].xCoordinate > width - (neighborsClass[5].yCoordinate / 2) || neighborsClass[5].yCoordinate < 0 || neighborsClass[5].yCoordinate > height || neighborsClass[5].zCoordinate < 0 || neighborsClass[5].zCoordinate >= 13) { neighborsClass[5].betretbar = false; }
		UnityEngine.Debug.Log(neighborsClass[5].xCoordinate + ", " + neighborsClass[5].yCoordinate + ", " + neighborsClass[5].zCoordinate + ", " + neighborsClass[5].currentSteps + ", " + neighborsClass[5].leastStepsToGoal);
		for (int i = 0; i < grid.HexList.Count; i++)
		{

			if (grid.HexList[i].xCoordinate == neighborsClass[5].xCoordinate && grid.HexList[i].yCoordinate == neighborsClass[5].yCoordinate && grid.HexList[i].zCoordinate == neighborsClass[5].zCoordinate)
			{

				if (grid.HexList[i].betretbar == false) { neighborsClass[5].betretbar = grid.HexList[i].betretbar; }
				break;

			}

		}

		return neighborsClass;

    }
}

public class Astar3coords
{

	Astar3coordsImportantMethods AstarIM = new Astar3coordsImportantMethods();

	public void Astar(Hex objectT, Hex start, Grid grid)
	{

		//int maxX = grid.gridWidth;
		//int maxY = grid.gridHeight;
		int maxX = 10000;
		int maxY = 10000;
		int objectX = objectT.xCoordinate;
		int objectY = objectT.yCoordinate;
		int objectZ = objectT.zCoordinate;
		int startX = start.xCoordinate;
		int startY = start.yCoordinate;
		int startZ = start.zCoordinate;

		Boolean abort = false;
		AstarHex current;
		List<AstarHex> neighbors = new List<AstarHex>();
		int[] neighborCost = new int[6];
		int noWay = 0;
		int oneNeighborCost = maxX * maxY;
		int debugcounter = 0;

		List<AstarHex> AstarHex = new List<AstarHex>();
		HashSet<AstarHex> ClosedList = new HashSet<AstarHex>();

		AstarHex.Add(new AstarHex() { xCoordinate = startX, yCoordinate = startY, zCoordinate = startZ, currentSteps = 0, leastStepsToGoal = AstarIM.distance(startX, startY, startZ, objectX, objectY, objectZ)});
		UnityEngine.Debug.Log("current Hex: " + AstarHex[0].xCoordinate + ", " + AstarHex[0].yCoordinate + ", " + AstarHex[0].zCoordinate + ";");

		while (AstarHex.Count > 0 && abort == false)
		{
			current = AstarHex[0];

			for (int i = 0; i < AstarHex.Count(); i++)
			{

				if (AstarHex[i].leastStepsToGoal + AstarHex[i].currentSteps <= current.leastStepsToGoal + current.currentSteps && AstarHex[i].leastStepsToGoal < current.leastStepsToGoal)
				{
					current = AstarHex[i];
				}

			}
			AstarHex.Remove(current);
			ClosedList.Add(current);
			UnityEngine.Debug.Log("current Hex: " + current.xCoordinate + ", " + current.yCoordinate + ", " + current.zCoordinate + ";");

			if (current.xCoordinate == objectX && current.yCoordinate == objectY && current.zCoordinate == objectZ) { abort = true; } //Abbruch wenn gefunden

			neighbors = AstarIM.neighbors(current.xCoordinate, current.yCoordinate, current.zCoordinate, objectX, objectY, objectZ, current.currentSteps, 10, 10, grid); //Richtige Width/Height noch nachliefern!

			UnityEngine.Debug.Log(neighbors.Count);

			for (int i = 0; i < neighbors.Count; i++)
            {

				//UnityEngine.Debug.Log(neighbors[i].xCoordinate + ", " + neighbors[i].yCoordinate + ", " + neighbors[i].zCoordinate);
				if (neighbors[i].betretbar == false || ClosedList.Contains(neighbors[i]))
                {

					UnityEngine.Debug.Log("continue?");
					continue;

                }
				if ((current.currentSteps + current.leastStepsToGoal) < (neighbors[i].currentSteps + neighbors[i].leastStepsToGoal) || AstarHex.Contains(neighbors[i]) == false)
                {

					neighbors[i].parent = current;
					AstarHex.Add(neighbors[i]);

                }

            }

		}


	}


}


public class AstarHex
{
	public int currentSteps;
	public int leastStepsToGoal;
	public int xCoordinate { get; set; }
	public int yCoordinate { get; set; }
	public int zCoordinate { get; set; }
	public Boolean betretbar = true;
	public AstarHex parent;


	public Boolean getBetretbar()
	{
		return betretbar;
	}


}