public void CalculateSeamsCost(int[,] energyMatrix, int Width, int Height, ref int minSeamValue, ref List<coord> seamPathCoord)
        {
            seamPathCoord = new List<coord>();
            int[,] seams = new int[Height, Width];
            for (int col = 0; col < Width; col++)
            {
                seams[0, col] = energyMatrix[0, col];
            }
            for (int row = 1; row < Height; )
            {
                for (int col = 0; col < Width; )
                {
                    if (col == 0)
                        seams[row, col] = Math.Min(seams[row - 1, col + 1], seams[row - 1, col]);
                    else if (col == Width - 1)
                        seams[row, col] = Math.Min(seams[row - 1, col - 1], seams[row - 1, col]);
                    else
                    {
                        int min = Math.Min(seams[row - 1, col - 1], seams[row - 1, col]);
                        seams[row, col] = Math.Min(min, seams[row - 1, col + 1]);
                    }


                   seams[row, col] += energyMatrix[row, col];
                    col++;
                }
                row++;
            }
            int MinimumVal = Int32.MaxValue;
            int IndexOfMin = -1;
            for (int col = 0; col < Width; )
            {
                if (seams[Height - 1, col] <= MinimumVal)
                {
                    IndexOfMin = col;
                    MinimumVal = seams[Height - 1, col];
                   
                }
                col++;
            }


            minSeamValue = MinimumVal;
            coord SeamElements;
            SeamElements.row = Height - 1;
            SeamElements.column = IndexOfMin;
            seamPathCoord.Add(SeamElements);

            for (int r = Height - 1; r > 0; r--)
            {

                SeamElements.row = r - 1;
                if (IndexOfMin == 0)
                {
                    if (seams[r - 1, IndexOfMin + 1] <= seams[r - 1, IndexOfMin])
                        IndexOfMin++;
                }
                else if (IndexOfMin == Width - 1)
                {
                    if (seams[r - 1, IndexOfMin - 1] <= seams[r - 1, IndexOfMin])
                        IndexOfMin--;
                }
                else
                {
                    int v1 = seams[r - 1, IndexOfMin];
                    int v2 = seams[r - 1, IndexOfMin - 1];
                    int v3 = seams[r - 1, IndexOfMin + 1];

                    if (v1 <= v2 && v1 <= v3)
                        IndexOfMin = IndexOfMin;
                    else if (v2 <= v1 && v2 <= v3)
                        IndexOfMin--;
                    else
                        IndexOfMin++;
                }
                SeamElements.column = IndexOfMin;
                seamPathCoord.Add(SeamElements);
            }

        }