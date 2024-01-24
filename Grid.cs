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
            
            for (int r = rows - 1; r >= 0; r--) {
                for (int c = 0; c < cols; c++) {
                    if (grid[r, c] > 0 && r < rows - 1 && grid[r + 1, c] == 0) {
                        newgrid[r + 1, c] = grid[r, c];     
                        newgrid[r, c] = 0f;
                    }                
                }
            }

            grid = newgrid;             
        }
    
    }
    
}