namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events; // Added for access 5.3

public class Enemy : Entity {
    // Field to hold reference to game event bus
    private GameEventBus? eventBus; // Null reference handling

    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    // Method to set the event bus reference
    public void SetGameEventBus(GameEventBus bus) {
        eventBus = bus;
    }

    // Method to handle enemy deletion with explosion
    public void DeleteWithExplosion() {
        if (eventBus != null) { // Hanldes null reference warning
            eventBus.RegisterEvent(
                new AddExplosionEvent(Shape.Position, Shape.Extent)
            );
        }
        
        DeleteEntity();
    }
}