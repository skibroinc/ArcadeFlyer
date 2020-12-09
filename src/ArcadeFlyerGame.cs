using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ArcadeFlyer2D
{
    // The Game itself
    class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        // The player
        private Player player;

        private PowerUp powerUp;

        //number of lives
        private int life = 3;

        private int score = 0;

        // A List of enemies
        private List<Enemy> enemies;

        // A timer for enemy generation
        private Timer enemyCreationTimer;

        // List of all projectiles on the screen
        private List<Projectile> projectiles;

        // Projectile image for player
        private Texture2D playerProjectileSprite;

        // Projectile image for enemy
        private Texture2D enemyProjectileSprite;

        //projectile image for power up
        private Texture2D powerUpProjectileSprite;
        
        public bool gameOver = false;

        //font
        private SpriteFont textfont;

        private int shotsFired = 0;

        private int totalAmmo = 50;

        private int ammoInClip = 5;
        
        private int totalShotsFired;

        // Screen width
        private int screenWidth = 1000;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }

        // Screen height
        private int screenHeight = 750;
        public int ScreenHeight
        {
            get { return screenHeight; }
            private set { screenHeight = value; }
        }

        // Initalized the game
        public ArcadeFlyerGame()
        {

            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            // Initialize the player to be in the top left
            player = new Player(this, new Vector2(0.0f, 0.0f));

            // Initialize empty list of enemies
            enemies = new List<Enemy>();

            // Initialize the power up to be on the left of the screen
            powerUp = new PowerUp(this, new Vector2(0.0f, 0.0f));
            
            // Add an enemy to be on the right side
            enemies.Add(new Enemy(this, new Vector2(screenWidth, 0)));

            // Initialize enemy creation timer
            enemyCreationTimer = new Timer(3.0f);
            enemyCreationTimer.StartTimer();

            // Initialize empty list of projectiles
            projectiles = new List<Projectile>();
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load in textures
            playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
            enemyProjectileSprite = Content.Load<Texture2D>("EnemyFire");
            powerUpProjectileSprite = Content.Load<Texture2D>("powerUpBulletFaceLeft");
        
            textfont = Content.Load<SpriteFont>("text");
        }


        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);

            //exit if ammo is 0
            if(totalAmmo == 0){
                gameOver = true;
                return;
            }

            //checks ammo in clip
            if(ammoInClip == 0){
                ammoInClip = 5;
            }

            //exit early if game over
            if(gameOver){
                return;
            }
            // Update the player
            player.Update(gameTime);

            // Update each enemy in the list
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            

            // Loop through projectiles backwards (in order to remove projectiles as needed)
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                // Update each projectile
                Projectile p = projectiles[i];
                p.Update();

                // Is this a player projectile?
                bool playerProjectile = p.ProjectileType == ProjectileType.Player;

                // Check if the player collides with this non-player projectile
                if (!playerProjectile && player.Overlaps(p))
                {
                    // There is a collision with the player, remove the projectile
                    projectiles.Remove(p);
                    //add if else statement for the power up projectile
                    life = life - 1;
                    if(life == 0){
                        gameOver = true;
                    }
                }
                else if (playerProjectile)
                {
                    // Loop through enemies backwards (in order to remove them as needed)
                    for (int enemyIdx = enemies.Count - 1; enemyIdx >= 0; enemyIdx--)
                    {
                        // Get the current enemy
                        Enemy enemy = enemies[enemyIdx];
                        // Check if this enemy collides with the player projectile
                        if (enemy.Overlaps(p))
                        {
                            // There is a collision with the enemy, remove the projectile
                            projectiles.Remove(p);

                            score = score + 1;

                            // Remove the enemy as well
                            enemies.Remove(enemy);
                        }
                    }
                }
            }
            
            // Enemy creation timer is up
            if (!enemyCreationTimer.Active)
            {
                // Add a new enemy to the list
                enemies.Add(new Enemy(this, new Vector2(screenWidth, 0.0f)));

                // Re-start the timer
                enemyCreationTimer.StartTimer();
            }

            // Update the enemy creation timer
            enemyCreationTimer.Update(gameTime);
        }

        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.White);

            // Start batch draw
            spriteBatch.Begin();

            // Draw the player
            if(!gameOver){
                player.Draw(gameTime, spriteBatch);
            }
            
            // Draw each enemy
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            // Draw all projectiles
            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
                
            }

            string scoreString = "Score: " + score.ToString();
            string livesString = "Lives: " + life.ToString();
            string ammo = totalAmmo.ToString();
            string clip = ammoInClip.ToString();
            spriteBatch.DrawString(textfont, scoreString, Vector2.Zero, Color.Black);
            spriteBatch.DrawString(textfont, livesString, new Vector2(0f, 20f), Color.Black);
            spriteBatch.DrawString(textfont, "Ammo: " + ammo, new Vector2(0f, 40f), Color.Black);
            spriteBatch.DrawString(textfont, "Ammo in clip: " + clip, new Vector2(0f, 60f), Color.Black);

            if(gameOver){
                totalShotsFired = shotsFired;
                spriteBatch.DrawString(textfont, "You Lose", new Vector2(screenWidth / 2, screenHeight / 2), Color.Red);
                spriteBatch.DrawString(textfont, "Final Score: " + score, new Vector2(screenWidth / 2 , screenHeight / 2 - 20), Color.Red);
                spriteBatch.DrawString(textfont, "Shots Fired: " + totalShotsFired, new Vector2(screenWidth / 2, screenHeight / 2 + 20), Color.Red);
            }

            // End batch draw
            spriteBatch.End();
        }

        // Fires a projectile with the given position and velocity
        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            // Create the image for the projectile
            Texture2D projectileImage;
            
            if (projectileType == ProjectileType.Player)
            {
                // This is a projectile sent from the player, set it to the proper sprite
                projectileImage = playerProjectileSprite;
                shotsFired += 1;
                totalAmmo -= 1;
                ammoInClip -= 1;
            }
            else if(projectileType == projectileType.PowerUp){
                projectileImage = powerUpProjectileSprite;
            }
            else
            {
                // This is a projectile sent from the enemy, set it to the proper sprite
                projectileImage = enemyProjectileSprite;
            }

            // Create the new projectile
            Projectile firedProjectile = new Projectile(position, velocity, projectileImage, projectileType);            

            // Add the projectile to the list
            projectiles.Add(firedProjectile);
        }
    }
}