using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Astar3coordsImportantMethods
{
	public int distance(int sourceX, int sourceY, int sourceZ, int objectX, int objectY, int objectZ)
	{
		
		return (Math.Abs(sourceX - objectX) + Math.Abs(sourceY - objectY) + Math.Abs(sourceZ - objectZ)) / 2;

	}
	public List<AstarHex> neighbors(int sourceX, int sourceY, int sourceZ, AstarHex astarHex)
    {
		List<AstarHex> neighborsClass = new List<AstarHex>(); //0-x, 1-y, 2-z

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX + 1, yCoordinate = sourceY, zCoordinate = sourceZ });
		if (astarHex.betretbar == false) { neighborsClass[0].xCoordinate = -1; }

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX, yCoordinate = sourceY + 1, zCoordinate = sourceZ });
		if (astarHex.betretbar == false) { neighborsClass[0].xCoordinate = -1; }

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX, yCoordinate = sourceY, zCoordinate = sourceZ + 1 });
		if (astarHex.betretbar == false) { neighborsClass[0].xCoordinate = -1; }

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX - 1, yCoordinate = sourceY, zCoordinate = sourceZ });
		if (astarHex.betretbar == false) { neighborsClass[0].xCoordinate = -1; }

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX, yCoordinate = sourceY + 1, zCoordinate = sourceZ });
		if (astarHex.betretbar == false) { neighborsClass[0].xCoordinate = -1; }

		neighborsClass.Add(new AstarHex() { xCoordinate = sourceX, yCoordinate = sourceY, zCoordinate = sourceZ - 1 });
		if (astarHex.betretbar == false) { neighborsClass[0].xCoordinate = -1; }

		return neighborsClass;

    }
}

public class Astar3coords
{

	Astar3coordsImportantMethods AstarIM = new Astar3coordsImportantMethods();

	public void Astar(Hex objectT, Hex start)
	{

		int maxX = grid.gridWidth;
		int maxY = grid.gridHeight;
		int objectX = objectT.xCoordinate;
		int objectY = objectT.yCoordinate;
		int objectZ = objectT.zCoordinate;
		int start = start.xCoordinate;
		int startY = start.yCoordinate;
		int startZ = start.zCoordinate;
		Queue closedList = new Queue();
		Boolean abort = false;
		AstarHex current = new AstarHex();
		int counter = 0;
		List<AstarHex> neighbors = new List<AstarHex>();
		int[] neighborCost = new int[6];
		int noWay = 0;
		int oneNeighborCost = maxX * maxY;
		//int neighborIndex = 0;
		int lowestPath = maxX * maxY;
		int lowestPathPos = 0;

		List<AstarHex> AstarHex = new List<AstarHex>();

		AstarHex.Add(new AstarHex() { xCoordinate = startX, yCoordinate = startY, zCoordinate = startZ, currentSteps = 0, leastStepsToGoal = AstarIM.distance(startX, startY, startZ, objectX, objectY, objectZ)});

		while (AstarHex.Count > 0 && abort == false)
		{
			noWay = 0;
			lowestPath = maxX * maxY;

			for (int i = 0; i < AstarHex.Count(); i++)
			{

				if (lowestPath > AstarHex[i].currentSteps + AstarHex[i].leastStepsToGoal)
				{

					lowestPath = AstarHex[i].currentSteps + AstarHex[i].leastStepsToGoal;
					lowestPathPos = i;

				}

			}
			current = AstarHex[lowestPathPos];
			UnityEngine.Debug.Log("current Hex: " + current.xCoordinate + ", " + current.yCoordinate + ", " + current.zCoordinate);
			closedList.Enqueue(current);
			AstarHex.Remove(current);
			if (current.xCoordinate == objectX && current.yCoordinate == objectY && current.zCoordinate == objectZ) { abort = true; }

			neighbors = AstarIM.neighbors(current.xCoordinate, current.yCoordinate, current.zCoordinate, current);

			for (int i = 0; i < 6; i++) {

				neighborCost[i] = AstarIM.distance(neighbors[i].xCoordinate, neighbors[i].yCoordinate, neighbors[i].zCoordinate, objectX, objectY, objectZ);

				if (closedList.Contains(neighbors[i]) && neighbors[i].xCoordinate != -1) {

					continue;

				}

				if (AstarHex.Contains(neighbors[i]) == false && neighbors[i].xCoordinate != -1)
				{

					AstarHex.Add(new AstarHex() { xCoordinate = neighbors[i].xCoordinate, yCoordinate = neighbors[i].yCoordinate, zCoordinate = neighbors[i].zCoordinate, currentSteps = counter, leastStepsToGoal = AstarIM.distance(current.xCoordinate, current.yCoordinate, current.zCoordinate, objectX, objectY, objectZ) });
					AstarHex[AstarHex.Count - 1].setPreviousA(current);
					AstarHex[AstarHex.Count - 1].currentSteps = AstarHex[AstarHex.Count - 1].previousA.currentSteps + 1;

				}
				else { 
				
					if(current.currentSteps + current.leastStepsToGoal < neighborCost[i].currentSteps + neighborCost[i].leastStepsToGoal && neighbors[i].xCoordinate != -1)
                    {

						AstarHex.Add(new AstarHex() { xCoordinate = neighbors[i].xCoordinate, yCoordinate = neighbors[i].yCoordinate, zCoordinate = neighbors[i].zCoordinate, currentSteps = counter, leastStepsToGoal = AstarIM.distance(current.xCoordinate, current.yCoordinate, current.zCoordinate, objectX, objectY, objectZ) });
						AstarHex[AstarHex.Count - 1].setPreviousA(current);
						AstarHex[AstarHex.Count - 1].currentSteps = AstarHex[AstarHex.Count - 1].previousA.currentSteps + 1;

					}

				}
			
			}
			
/**			if (noWay == 6 && abort == false) { 
			
				for (int i = 0; i < 6; i++)
				{

					if (neighborCost[i] < oneNeighborCost)
					{

						oneNeighborCost = neighborCost[i];
						neighborIndex = i;

					}

				}

				AstarHex.Add(new AstarHex() { xCoordinate = neighbors[neighborIndex, 0], yCoordinate = neighbors[neighborIndex, 1], zCoordinate = neighbors[neighborIndex, 2], currentSteps = 0, leastStepsToGoal = AstarIM.distance(current.xCoordinate, current.yCoordinate, current.zCoordinate, objectX, objectY, objectZ) });

			} **/

		}


	}


}


public class AstarHex : Hex
{
	public int zCoordinate;
	public int currentSteps;
	public int leastStepsToGoal;
	public AstarHex previousA = new AstarHex();

	public AstarHex getPreviousA()
	{
		if (previous == null)
			return null;
		else
			return previousA;
	}

	public void setPreviousA(AstarHex prev)
	{
		previousA = prev;
	}

}