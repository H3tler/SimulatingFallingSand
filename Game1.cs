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
    float hsv = 0;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Height = graphics.PreferredBackBufferHeight = 1000;
        Width = graphics.PreferredBackBufferWidth = 800;

        graphics.ApplyChanges();

        //Height = graphics.PreferredBackBufferHeight;
        //Width = graphics.PreferredBackBufferWidth;

        size = 10;

        grid = new(Height / size, Width / size);
        



        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData<Color>(new Color[] { Color.White });

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

            if (x >= grid.cols) {
                x = grid.cols - 1;
            }
            else if (x < 0) {
                x = 0;
            }
            if (y >= grid.rows) {
                y = grid.rows - 1;
            }
            else if (y < 0) {
                y = 0;
            }
            
            if (hsv > 360) hsv = 0;
            grid.SetGrid(x ,y, hsv);
            hsv++;
        } 
    }

    public static Color HsvToRgb(float h, float s, float v) // Copied from the internet :)
    {
        float r, g, b;
        int i;
        float f, p, q, t;
        if (s == 0)
        {
            return Color.Gray;
        }
        h /= 60;           // sector 0 to 5
        i = (int)h;
        f = h - i;         // factorial part of h
        p = v * (1 - s);
        q = v * (1 - s * f);
        t = v * (1 - s * (1 - f));
        switch (i)
        {
            case 0:
                r = v;
                g = t;
                b = p;
                break;
            case 1:
                r = q;
                g = v;
                b = p;
                break;
            case 2:
                r = p;
                g = v;
                b = t;
                break;
            case 3:
                r = p;
                g = q;
                b = v;
                break;
            case 4:
                r = t;
                g = p;
                b = v;
                break;
            default:       // case 5:
                r = v;
                g = p;
                b = q;
                break;
        }

        return new Color(r, g, b, 255f);
    }

    
}
