namespace Galaga;

using System.Numerics;

public readonly struct AddExplosionEvent {
    public Vector2 Position { get; }
    public Vector2 Extent { get; }

    public AddExplosionEvent(Vector2 position, Vector2 extent) {
        Position = position;
        Extent = extent;
    }
}