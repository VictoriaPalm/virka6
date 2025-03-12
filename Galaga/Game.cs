namespace Galaga;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using System.Numerics;
using System.Collections.Generic;
using DIKUArcade.Events;
using Galaga.Squadron;
using Galaga.Movement;
using Galaga.Hit;

public class Game : DIKUGame {
    // Game entities
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;

    // Explosion handling components
    private GameEventBus gameEventBus;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    // Squadron handling
    private ISquadron[] squadrons; // Array of different formations
    private int currentSquadronIndex;
    private ISquadron squadron;

    // Enemy graphics
    private List<Image> enemyStrides;
    private List<Image> enragedStrides;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // Creates player at the bottom center of the screen
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                             new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));

        // Load enemy animation frames
        enemyStrides = ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png");
        enragedStrides = ImageStride.CreateStrides(4, "Galaga.Assets.Images.RedMonster.png");

        // Setup available squadron formations
        squadrons = new ISquadron[] {
            new VFormationSquadron(),
            new LineFormationSquadron(),
            new CircleFormationSquadron()
        };
        currentSquadronIndex = 0;

        // Setup explosion system
        gameEventBus = new GameEventBus();
        gameEventBus.Subscribe<AddExplosionEvent>(AddExplosion);
        enemyExplosions = new AnimationContainer(10);
        explosionStrides = ImageStride.CreateStrides(8, "Galaga.Assets.Images.Explosion.png");

        // Create initial squadron formation
        ChangeSquadron();

        // Setup player shooting
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png");
    }

    // Cleans up event subscriptions when game is destroyed
    ~Game() {
        gameEventBus.Unsubscribe<AddExplosionEvent>(AddExplosion);
    }

    // Change squadron formation
    private void ChangeSquadron() {
        currentSquadronIndex = (currentSquadronIndex + 1) % squadrons.Length;
        squadron = squadrons[currentSquadronIndex];

        // Create new enemies based on squadron formation
        enemies = squadron.CreateEnemies(
            enemyStrides,
            enragedStrides,
            () => new NoMoveStrategy(),
            () => new BasicHitStrategy()
        );
    }

    // Handles movement, collisions and shots
    private void IterateShots() {
        playerShots.Iterate(shot => {
            // Move shot upward
            shot.Shape.Move();

            // Removes shot if it goes off screen
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            } else {
                // Checks for collisions with enemies
                enemies.Iterate(enemy => {
                    var collision = CollisionDetection.Aabb(
                        shot.Shape.AsDynamicShape(),
                        enemy.Shape
                    );

                    // Handles collision
                    if (collision.Collision) {
                        shot.DeleteEntity();
                        enemy.DeleteWithExplosion();
                    }
                });
            }
        });
    }

    // Renders all game elements
    public override void Render(WindowContext context) {
        window.Clear();
        player.RenderEntity(context);
        enemies.RenderEntities(context);
        playerShots.RenderEntities(context);
        enemyExplosions.RenderAnimations(context);
    }

    // Updates game state each frame
    public override void Update() {
        player.Move();
        IterateShots();
        gameEventBus.ProcessEvents();
    }

    // Creates explosion animation when an enemy is destroyed
    public void AddExplosion(AddExplosionEvent addExplosionEvent) {
        enemyExplosions.AddAnimation(
            new StationaryShape(addExplosionEvent.Position, addExplosionEvent.Extent),
            EXPLOSION_LENGTH_MS / 8,
            new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
    }

    // Handles keyboard input
    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // Forward movement keys to player
        player.KeyHandler(action, key);

        // Creates shot when Space is released
        if (action == KeyboardAction.KeyRelease && key == KeyboardKey.Space) {
            // Position shot at top center of player
            Vector2 playerPos = player.GetPosition();
            Vector2 shotPos = new Vector2(
                playerPos.X + player.Shape.Extent.X / 2f,
                playerPos.Y + player.Shape.Extent.Y
            );
            playerShots.AddEntity(new PlayerShot(shotPos, playerShotImage));
        }

        // Switch squadron formation when 'S' is pressed
        if (action == KeyboardAction.KeyPress && key == KeyboardKey.S) {
            ChangeSquadron();
        }
    }
}
