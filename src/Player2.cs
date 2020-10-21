using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D{
    class player2{
        public Player2()
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
            
            

            public player2(){

            }
        }
    }
}