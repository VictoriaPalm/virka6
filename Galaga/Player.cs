namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

public class Player : Entity {

    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        
    }

    public void Move() {
        // TODO: move the shape and guard against the window borders
    }

    public void SetMoveLeft(bool val) {
        // TODO:set moveLeft appropriately and call UpdateVelocity()
    }

    public void SetMoveRight(bool val) {
        // TODO:set moveRight appropriately and call UpdateVelocity()
    }

}
