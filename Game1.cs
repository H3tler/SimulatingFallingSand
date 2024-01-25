global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
using System;

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
    int rad = 1;
    int scrollvalue;

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

        size = 5;

        grid = new(Height / size, Width / size);

        scrollvalue = Mouse.GetState().ScrollWheelValue;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new Color[] { Color.White });

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
     
        AdjustPlacementSize();
        MouseInput();
        grid.Gravity();
        
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
         
            hsv++; 
            if (hsv >= 360) hsv = 1;
                                                 
            //grid.SetGrid(x ,y, hsv);

            for (float i = 0; i <= Math.PI * 2; i += 0.05f) {
                for (int j = 0; j < rad; j++) {
                    var rot = GetRotation(i, j);
                    var X = x + rot.X;
                    var Y = y + rot.Y;
                    var FinalVec = OutofBound((int)X, (int)Y);
                    if (grid.grid[(int)FinalVec.Y, (int)FinalVec.X] == 0) {
                        grid.SetGrid((int)FinalVec.X, (int)FinalVec.Y, hsv);                       
                    }                                
                }                
            }
                        
        } 

        if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
            grid.ResetGrid();
        }
    }

    public static Vector2 GetRotation(float angle, float radius)
    {
        float x = MathF.Cos(angle) * radius;
        float y = MathF.Sin(angle) * radius;

        return new Vector2(x, y);
    }

    public Vector2 OutofBound(int x, int y) 
    {
        if (x < 0) x = 0;
        else if (x >= grid.cols) x = grid.cols - 1;
        if (y < 0) y = 0;
        else if (y >= grid.rows) y = grid.rows - 1;

        return new Vector2(x, y);
    }

    public void AdjustPlacementSize()
    {
        var mstate = Mouse.GetState();

        if (mstate.ScrollWheelValue < scrollvalue) {
            rad--;
            if (rad < 1) rad = 1;            
        }
        if (mstate.ScrollWheelValue > scrollvalue) {
            rad++;
            if (rad > 25) rad = 25;
        }
        scrollvalue = mstate.ScrollWheelValue;
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
