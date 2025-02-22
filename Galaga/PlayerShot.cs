namespace Galaga;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class PlayerShot : Entity {
    private static Vector2 extent = new Vector2(0.008f, 0.021f);
    private static Vector2 velocity = new Vector2(0.0f, 0.1f);

    // Modified constructor to take position and image as specified 5.2.4
    public PlayerShot(Vector2 position, IBaseImage image) : base(new DynamicShape(position, extent), image) {
        // Sets the velocity for the shot 5.2.4
        Shape.AsDynamicShape().Velocity = velocity;
    }
}