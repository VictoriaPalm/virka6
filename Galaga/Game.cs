namespace Galaga;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;  // Added to access DynamicShape 5.2.1
using DIKUArcade.Graphics;  // Added to access Image 5.2.1
using System.Numerics;      // Added to access Vector2 5.2.1
using System.Collections.Generic;  // Added to import List<T> type 5.2.3

public class Game : DIKUGame {
   private Player player;
   // Container to manage multiple enemy entities 5.2.3 (copied)
   private EntityContainer<Enemy> enemies;

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
   }
   
   // Renders the player using the inherited RenderEntity method from Entity class
   public override void Render(WindowContext context) {
       player.RenderEntity(context);
       // Render all enemies in the container 5.2.3
       enemies.RenderEntities(context);
   }

   // Implemented as part of 5.2.
   public override void Update() {
        player.Move();
   }

   // Will be implemented in later steps when we add keyboard controls
   public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
   }
}