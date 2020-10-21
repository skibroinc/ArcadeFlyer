using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class Player
    {
        private float spriteWidth;
        private ArcadeFlyerGame root;
        private Vector2 position; 
        private Texture2D spriteImage;
        private float movementSpeed = 4.0f;


        public float SpriteHeight{
            get{
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
        }

        public Rectangle PostionRectangle{
            get{
                Rectangle rec = new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
                return rec;
            }
        }

        public Player(ArcadeFlyerGame root, Vector2 position){
            this.root = root;
            this.position = position;
            this.spriteWidth = 128.0f;

            LoadContent();
        }

        public void LoadContent(){
            this.spriteImage = root.Content.Load<Texture2D>("MainChar");
        }

        public void update(GameTime gameTime){
            KeyboardState currentKeyboardState = Keyboard.GetState();
            HandleInput(currentKeyboardState);
        }

        private void HandleInput(KeyboardState currentKeybordState){
            bool wKeyPressed = currentKeybordState.IsKeyDown(Keys.W);
            if(wKeyPressed){
                position.Y -= movementSpeed;
            }
            bool sKeyPressed = currentKeybordState.IsKeyDown(Keys.S);
            if(sKeyPressed){
                position.Y += movementSpeed;
            }
            bool aKeyPressed = currentKeybordState.IsKeyDown(Keys.A);
            if(aKeyPressed){
                position.X -= movementSpeed;
            }
            bool dKeyPressed = currentKeybordState.IsKeyDown(Keys.D);
            if(dKeyPressed){
                position.X += movementSpeed;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            spriteBatch.Draw(spriteImage, PostionRectangle, Color.White);
        }
    }
}