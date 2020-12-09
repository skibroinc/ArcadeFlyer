using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArcadeFlyer2D{
    class PowerUp : Sprite{
        private ArcadeFlyerGame root;
        private Vector2 velocity;
        private Timer powerUpCooldown;

        private Random rand = new Random(); 

        public PowerUp(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            // Initialize values
            this.root = root;
            this.position = position;
            this.SpriteWidth = 20.0f;
            this.velocity = new Vector2(-5.0f, 0.0f);
            this.powerUpCooldown = new Timer(10.0f);

            // Load the content for power up
            LoadContent();
        }

        public void LoadContent()
        {
            // Get the Enemy image
            this.SpriteImage = root.Content.Load<Texture2D>("powerUpBulletFaceLeft");
        }

        public void Update(GameTime gameTime){
            position += velocity;

            if(!powerUpCooldown.Active){
                //make random y-cordinate for position
                int powerUpPosition = rand(20.0f, 700.0f);
                Vector2 projectilePosition = new Vector2(1000.0f, powerUpPosition);
                root.FireProjectile(projectilePosition, -5.0, ProjectileType.PowerUp);

                powerUpCooldown.StartTimer();
            }

            powerUpCooldown.Update(gameTime);
        }
    }
}
//power up ideas- temp invize, percing shot, enemy stun, nuke,

//to do- make sprite appear, make it move, make it dissappear when it hits player, and gives the player a random power up