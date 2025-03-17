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
    private static Game instance; 

    // Game entities
    private Player player;
    private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>();
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;

    // Explosion handling
    private GameEventBus gameEventBus; 
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    // Squadron handling
    private ISquadron[] squadrons;
    private int currentSquadronIndex;
    private ISquadron squadron = new VFormationSquadron(); 

    // Enemy graphics
    private List<Image> enemyStrides;
    private List<Image> enragedStrides;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        instance = this; 

        // Initialize player
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                             new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));

        // Load enemy animations
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

        // Setup player shooting
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png");

        // Initialize squadron
        ChangeSquadron();
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        System.Diagnostics.Debug.WriteLine($"Key pressed: {key}");

        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.Left:
                    player.SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(true);
                    break;
                case KeyboardKey.Space:
                    player.Shoot(playerShots, playerShotImage);
                    break;
                case KeyboardKey.S:
                    ChangeSquadron();
                    break;
            }
        } else if (action == KeyboardAction.KeyRelease) {
            switch (key) {
                case KeyboardKey.Left:
                    player.SetMoveLeft(false);
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(false);
                    break;
            }
        }
    }


    public static Game GetInstance() {
        return instance;
    }

    public GameEventBus GetGameEventBus() {
        return gameEventBus;
    }
    
    private void ChangeSquadron() {
        System.Diagnostics.Debug.WriteLine($"Changing squadron...");

        currentSquadronIndex = (currentSquadronIndex + 1) % squadrons.Length;
        squadron = squadrons[currentSquadronIndex];

        enemies = squadron.CreateEnemies(enemyStrides, enragedStrides, 
                                        () => new NoMoveStrategy(), 
                                        () => new BasicHitStrategy());

        System.Diagnostics.Debug.WriteLine($"New squadron: {squadron.GetType().Name}");
    }


    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    var collision = CollisionDetection.Aabb(
                        shot.Shape.AsDynamicShape(),
                        enemy.Shape
                    );

                    if (collision.Collision) {
                        shot.DeleteEntity();
                        enemy.TakeDamage(1);
                    }
                });
            }
        });
    }

    public override void Render(WindowContext context) {
        window.Clear();
        player.RenderEntity(context);
        enemies.RenderEntities(context); 
        playerShots.RenderEntities(context);
        enemyExplosions.RenderAnimations(context);

        System.Diagnostics.Debug.WriteLine($"Rendering {enemies.CountEntities()} enemies"); 
    }

    public override void Update() {
        player.Move();
        IterateShots();
        gameEventBus.ProcessEvents();
    }

    public void AddExplosion(AddExplosionEvent addExplosionEvent) {
        enemyExplosions.AddAnimation(
            new StationaryShape(addExplosionEvent.Position, addExplosionEvent.Extent),
            EXPLOSION_LENGTH_MS / 8,
            new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
    }
}
