using System;
using System.Collections.Generic;
using System.Reflection;

namespace FallingSandSimulator
{
    public class Grid
    {
        float[,] _grid;   
        public int rows {get; private set;}
        public int cols {get; private set;}
        int size;
        public float[,] grid {
            get {return _grid;}
        }

        public Grid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            _grid =  new float[rows, cols];

            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    _grid[r, c] = 0;
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
            _grid[y, x] = val;
        }

        public void Gravity()
        {

        }
    }
}