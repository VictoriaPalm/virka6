namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public class Enemy : Entity {
    private GameEventBus? eventBus;
    private int health = 3;

    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
        System.Diagnostics.Debug.WriteLine($"Enemy created with image type: {image.GetType()}");
    }


    public void SetGameEventBus(GameEventBus bus) {
        eventBus = bus;
    }

    public void DeleteWithExplosion() {
        if (eventBus != null) {
            eventBus.RegisterEvent(new AddExplosionEvent(Shape.Position, Shape.Extent));
        } else {
            System.Diagnostics.Debug.WriteLine("ERROR: eventBus is NULL!");
        }

        DeleteEntity();
    }

    public void TakeDamage(int damage) {
        health -= damage;
        System.Diagnostics.Debug.WriteLine($"Enemy took {damage} damage! Health left: {health}");

        if (health <= 0) {
            System.Diagnostics.Debug.WriteLine("Enemy should explode now!");
            DeleteWithExplosion();
        }
    }
}
