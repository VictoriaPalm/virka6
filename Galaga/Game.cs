namespace Galaga;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;  // Added to access DynamicShape 5.2.1
using DIKUArcade.Graphics;  // Added to access Image 5.2.1
using DIKUArcade.Physics;   // Added to access CollisionDetection 5.2.4
using System.Numerics;      // Added to access Vector2 5.2.1
using System.Collections.Generic;  // Added to import List<T> type 5.2.3

public class Game : DIKUGame {
    private Player player;
    // Container to manage multiple enemy entities 5.2.3 (copied)
    private EntityContainer<Enemy> enemies;
    // Container and image for player shots 5.2.4 (copied)
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;

    // Copied from SU25_A4 5.2.1
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));

        // Creates image strides for enemy animation 5.2.3 (copied)
        List<Image> images =
            ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png");
       
        // Sets number of enemies and create container 5.2.3 (copied)
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);

        // Populates enemies across the top of the screen 5.2.3 (copied)
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vector2(0.1f + (float)i * 0.1f, 0.9f),
                    new Vector2(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }

        // Initializes container and image for player shots 5.2.4 (copied)
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png");
    }

    // Processes all active shots, handles collisions 5.2.4
    private void IterateShots() {
        playerShots.Iterate(shot => {
            // Moves the shot upward 5.2.4
            shot.Shape.Move();
            
            // Deletes shot if it moves beyond top of screen 5.2.4
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    // Checks for collision between shot and enemy 5.2.4
                    var collision = CollisionDetection.Aabb(
                        shot.Shape.AsDynamicShape(),
                        enemy.Shape
                    );
                    
                    // If collision occurred, removes both shot and enemy 5.2.4
                    if (collision.Collision) {
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }
   
    // Renders the player using the inherited RenderEntity method from Entity class
    public override void Render(WindowContext context) {
        window.Clear();
        player.RenderEntity(context);
        enemies.RenderEntities(context);
        // Renders all active player shots 5.2.4
        playerShots.RenderEntities(context);
    }

    // Implemented as part of 5.2.2
    public override void Update() {
        player.Move();
        // Processes player shots movement and collisions 5.2.4
        IterateShots();
    }

    // Adds player keyboard control handling 5.2.2
    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        player.KeyHandler(action, key);
        
        // Creates new shot when Space is released 5.2.4
        if (action == KeyboardAction.KeyRelease && key == KeyboardKey.Space) {
            // Gets player position and creates shot slightly above center of player 5.2.4
            Vector2 playerPos = player.GetPosition();
            Vector2 shotPos = new Vector2(
                playerPos.X + player.Shape.Extent.X/2f,  // Centers horizontally
                playerPos.Y + player.Shape.Extent.Y      // Positions at top of player
            );
            playerShots.AddEntity(new PlayerShot(shotPos, playerShotImage));
        }
    }
}