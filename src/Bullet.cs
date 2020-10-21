using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D{
    class Bullet{
        private float bulletWidth;
        private ArcadeFlyerGame root;
        private Texture2D spriteImage;
        private Vector2 position;
        private float bulletSpeed = 10.0f;

        public float bulletHeight{
            get{
                float scale = bulletWidth / (spriteImage.Width * 10);
                return spriteImage.Height * scale;
            }
        }

        public Rectangle PostionRectangle{
            get{
                Rectangle rec = new Rectangle((int)position.X, (int)position.Y, (int)bulletWidth, (int)bulletHeight);
                return rec;
            }
        }

        public Bullet(ArcadeFlyerGame root, Vector2 position){
            this.root = root;
            this.position = position;
            this.bulletWidth = 12.8f;

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
            bool capsLockKeyPressed = currentKeybordState.IsKeyDown(Keys.CapsLock);
            if(capsLockKeyPressed){
                position.X += bulletSpeed;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            spriteBatch.Draw(spriteImage, PostionRectangle, Color.White);
        }
    }
}