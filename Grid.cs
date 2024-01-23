namespace FallingSandSimulator
{
    public class Grid
    {
        byte[,] _grid;   
        int rows;
        int cols;
        int size;
        public byte[,] grid {
            get {return _grid;}
            set {}
        }

        public Grid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            _grid =  new byte[rows, cols];

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
                        spriteBatch.Draw(texture, new Rectangle(c * size, r * size, size, size), Color.White);
                    }         
                }
            }
        }

        public void SetGrid(int x, int y, byte val)
        {
            _grid[y, x] = val;
        }
    }
}