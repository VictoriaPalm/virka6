namespace Galaga;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class PlayerShot : Entity {
    // Size and speed for all shots
    private static Vector2 extent = new Vector2(0.008f, 0.021f);
    private static Vector2 velocity = new Vector2(0.0f, 0.1f);

    // Creates a new shot at the specified position
    public PlayerShot(Vector2 position, IBaseImage image) 
        : base(new DynamicShape(position, extent), image) {
        // Sets upward movement
        Shape.AsDynamicShape().Velocity = velocity;
    }
}