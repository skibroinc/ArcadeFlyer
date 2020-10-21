using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class player2
    {
        public player2
        {
            private float spriteWidth;
            private ArcadeFlyerGame root;
            private Vector2 position; 
            private Texture2D spriteImage;
            private float movementSpeed = 4.0f;
            private Vector2 velocity;

            public float SpriteHeight{
                get{
                    float scale = spriteWidth / spriteImage.Width;
                    return spriteImage.Height * scale;
                }
            }
            public Rectangle PositionRectangle
            {
                get {
                    Rectangle rec = new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
                    return rec;
                }
            }
            
            

            public player2(ArcadeFlyerGame root, Vector2 position)
            {
                this.root = root;
                this.position = position;
                this.spriteWidth = 128.0f;
                this.velocity = new Vector2(-1.0f, 5.0f);

                LoadContent();
            }

            public void LoadContent()
            {
                this.spriteImage = root.Content.Load<Texture2D>("New Piskel");
            }

            public void Update(GameTime gameTime)
            {
                position += velocity;

                if (position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
                {
                    velocity.Y *= -1;
                }
            }

            public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
            }


        }
    }
}