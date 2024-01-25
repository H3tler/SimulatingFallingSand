using System;

namespace FallingSandSimulator
{
    public class Grid
    {
        public float[,] grid {get; private set;}   
        public int rows {get; private set;}
        public int cols {get; private set;}
        int size;

        public Grid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            grid =  new float[rows, cols];

            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    grid[r, c] = 0;
                }
            }
        }

        public void DrawGrid(SpriteBatch spriteBatch, Texture2D texture, int size)
        {  
            for (int r = 0; r < rows; r++){
                for (int c = 0; c < cols; c++) {
                    if (grid[r, c] > 0) {
                        Color color = Game1.HsvToRgb(grid[r, c], 1f, 1f);                        
                        spriteBatch.Draw(texture, new Rectangle(c * size, r * size, size, size), color);
                    }         
                }
            }
        }

        public void SetGrid(int x, int y, float val)
        {
            grid[y, x] = val;
        }

        public void ResetGrid()
        {
            for (int r = 0; r < rows; r++){
                for (int c = 0; c < cols; c++) {
                    grid[r, c] = 0;
                }
            }
        }

        public void Gravity()
        {
            float[,] newgrid = grid;
            bool fell = false;
            
            for (int r = rows - 1; r >= 0; r--) {
                for (int c = 0; c < cols; c++) {
                    if (newgrid[r, c] > 0 && r < rows - 1 && newgrid[r + 1, c] == 0) {
                        fell = false;
                        if (newgrid[r + 1, c] == 0) {
                            newgrid[r + 1, c] = newgrid[r, c];                      
                            newgrid[r, c] = 0f; 
                            fell = true;
                        }                                                                 
                    }
                    if (r + 2 < rows && newgrid[r + 2, c] > 0 && fell == true) newgrid = PushToSide(newgrid, new Vector2(c, r + 1));  
                }                         
            }

            grid = newgrid;         
        }

        private float[,] PushToSide(float[,] someGrid, Vector2 pos)
        {
            Random ran = new();
            int val = ran.Next(0, 2);
            int y = (int)pos.Y;
            int x = (int)pos.X;
            
            if (val == 1) {
                int xx = 0;
                for (int i = x; i < cols - 1; i++) {
                    if (someGrid[y, i + 1] == 0) {
                        xx = i + 1;
                        break;
                    }                   
                }
                if (xx != 0) {
                    for (int i = x; i < xx; i++) {
                        if (someGrid[y, i + 1] == 0) {
                            someGrid[y, i + 1] = someGrid[y, i];
                            someGrid[y, i] = 0;
                        }            
                    }
                }
            }   
            else {
                int xx = cols;
                for (int i = x; i > 0; i--) {
                    if (someGrid[y, i - 1] == 0) {
                        xx = i - 1;
                        break;
                    }                   
                }
                if (xx != cols) {
                    for (int i = x; i > xx; i--) {
                        if (someGrid[y, i - 1] == 0) {
                            someGrid[y, i - 1] = someGrid[y, i];
                            someGrid[y, i] = 0;
                        }            
                    }
                }
            }
            
            return someGrid;
        }
    
    }
    
}