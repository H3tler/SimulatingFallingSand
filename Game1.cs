global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;

namespace FallingSandSimulator;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    Grid grid;
    public int Height, Width;
    Texture2D pixel;
    int size;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferHeight = 1000;
        graphics.PreferredBackBufferWidth = 800;

        graphics.ApplyChanges();

        Height = graphics.PreferredBackBufferHeight;
        Width = graphics.PreferredBackBufferWidth;

        size = 10;

        grid = new(Height / size, Width / size);
        



        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData<Color>(new Color[] { Color.White });

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        MouseInput();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        spriteBatch.Begin();

        grid.DrawGrid(spriteBatch, pixel, size);

        spriteBatch.End();

        base.Draw(gameTime);
    }

    public void MouseInput()
    {
        var mstate = Mouse.GetState();

        if (mstate.LeftButton == ButtonState.Pressed) {
            int x = mstate.X / size;
            int y = mstate.Y / size;

            if (x >= grid.grid.GetLength(1)) {
                x = grid.grid.GetLength(1) - 1;
            }
            else if (x < 0) {
                x = 0;
            }
            if (y >= grid.grid.GetLength(0)) {
                y = grid.grid.GetLength(0) - 1;
            }
            else if (y < 0) {
                y = 0;
            }
            

            grid.SetGrid(x ,y, 1);
        }
        
        
    }
}
