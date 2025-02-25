namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public class Enemy : Entity {
    // Field to store reference to the game's event system
    private GameEventBus? eventBus;

    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    // Sets the event bus for this enemy
    public void SetGameEventBus(GameEventBus bus) {
        eventBus = bus;
    }

    // Creates explosion and removes enemy
    public void DeleteWithExplosion() {
        if (eventBus != null) {
            eventBus.RegisterEvent(
                new AddExplosionEvent(Shape.Position, Shape.Extent)
            );
        }
        
        DeleteEntity();
    }
}