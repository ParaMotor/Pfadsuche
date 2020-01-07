using System;

namespace Astar
{
    class Astar{

        Boolean hitEndPoint = false;
        int x;
        int y;

        public void search(int endX, int endY, AstarOpenList openList) {

            while (hitEndPoint == false) {

                openList.removeMinimum();

                if (x == endX && y == endY) {

                    hitEndPoint = true;
                
                }
            
            }

        }

    }


    class AstarOpenList {

        int[] x = new int[6];
        int[] y = new int[6];
        int[] currentMovementCost = new int[6];
        int[] estimatedMovementCost = new int[6];
        int[] computingScore = new int[6];

        public void removeMinimum() { 
        

        
        }

        public void generateScore(int currentXlocation, int currentYlocation, int maxX, int maxY) {

            if ((currentXlocation % 2 != 0 && currentYlocation % 2 != 0) || (currentXlocation % 2 == 0 && currentYlocation % 2 != 0)) { //ungerade, ungerade und gerade, ungerade

                x[0] = currentXlocation; //links oben
                y[0] = currentYlocation - 1; //links oben

                x[1] = currentXlocation + 1; //rechts oben
                y[1] = currentXlocation - 1; //rechts oben

                x[2] = currentXlocation + 1;  //rechts
                y[2] = currentXlocation; //rechts

                x[3] = currentXlocation + 1; //rechts unten
                y[3] = currentXlocation + 1; //rechts unten

                x[4] = currentXlocation; //links unten
                y[4] = currentXlocation + 1; //links unten

                x[5] = currentXlocation - 1; //links
                y[5] = currentXlocation; //links


                if (currentYlocation - 1 < 0 && currentXlocation - 1 >= 0) { //oben keine (y = 0)

                    x[0] = -1; //links oben
                    y[0] = -1; //links oben

                    x[1] = -1; //rechts oben
                    y[1] = -1; //rechts oben

                }

                else if (currentYlocation - 1 >= 0 && currentXlocation - 1 < 0) { //links keine (x = 0)

                    x[0] = -1; //links oben
                    y[0] = -1; //links oben

                    x[4] = -1; //links unten
                    y[4] = -1; //links unten

                    x[5] = -1; //links
                    y[5] = -1; //links

                }

                else if (currentYlocation - 1 <= maxY && currentXlocation - 1 >= 0) { //unten keine (y = MAX)

                    x[3] = -1; //rechts unten
                    y[3] = -1; //rechts unten

                    x[4] = -1; //links unten
                    y[4] = -1; //links unten

                }

                else if (currentYlocation - 1 >= 0 && currentXlocation - 1 <= maxX) { //rechts keine (x = MAX)

                    x[3] = -1; //rechts unten
                    y[3] = -1; //rechts unten

                    x[4] = -1; //links unten
                    y[4] = -1; //links unten


                }
            }


            if ((currentXlocation % 2 == 0 && currentYlocation % 2 == 0) || (currentXlocation % 2 != 0 && currentYlocation % 2 == 0)) { //gerade, gerade und ungerade, gerade

                x[0] = currentXlocation - 1; //links oben
                y[0] = currentYlocation - 1; //links oben

                x[1] = currentXlocation; //rechts oben
                y[1] = currentXlocation - 1; //rechts oben

                x[2] = currentXlocation + 1;  //rechts
                y[2] = currentXlocation; //rechts

                x[3] = currentXlocation; //rechts unten
                y[3] = currentXlocation + 1; //rechts unten

                x[4] = currentXlocation - 1; //links unten
                y[4] = currentXlocation + 1; //links unten

                x[5] = currentXlocation - 1; //links
                y[5] = currentXlocation; //links


                if (currentYlocation - 1 < 0 && currentXlocation - 1 >= 0)
                { //oben keine (y = 0)

                    x[0] = -1; //links oben
                    y[0] = -1; //links oben

                    x[1] = -1; //rechts oben
                    y[1] = -1; //rechts oben

                }

                else if (currentYlocation - 1 >= 0 && currentXlocation - 1 < 0)
                { //links keine (x = 0)

                    x[0] = -1; //links oben
                    y[0] = -1; //links oben

                    x[4] = -1; //links unten
                    y[4] = -1; //links unten

                    x[5] = -1; //links
                    y[5] = -1; //links

                }

                else if (currentYlocation - 1 <= maxY && currentXlocation - 1 >= 0)
                { //unten keine (y = MAX)

                    x[3] = -1; //rechts unten
                    y[3] = -1; //rechts unten

                    x[4] = -1; //links unten
                    y[4] = -1; //links unten

                }

                else if (currentYlocation - 1 >= 0 && currentXlocation - 1 <= maxX)
                { //rechts keine (x = MAX)

                    x[3] = -1; //rechts unten
                    y[3] = -1; //rechts unten

                    x[4] = -1; //links unten
                    y[4] = -1; //links unten


                }
            }
        
        }
    
    }

    class AstarClosedList {

        int x;
        int y;
    
    }
}
