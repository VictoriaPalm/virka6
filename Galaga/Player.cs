namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.Numerics;
using System;

public class Player : Entity {
    // Movement variables
    private float moveLeft;
    private float moveRight;
    private const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        // Initializes movement to zero
        moveLeft = 0.0f;
        moveRight = 0.0f;
    }

    // Gets player position for shot placement
    public Vector2 GetPosition() {
        return Shape.Position;
    }
    
    // Controls left movement
    public void SetMoveLeft(bool move) {
        moveLeft = move ? -MOVEMENT_SPEED : 0.0f;
        UpdateVelocity();
    }

    // Controls right movement
    public void SetMoveRight(bool move) {
        moveRight = move ? MOVEMENT_SPEED : 0.0f;
        UpdateVelocity();
    }

    // Calculates total movement velocity
    private void UpdateVelocity() {
        Shape.AsDynamicShape().Velocity = new Vector2(moveLeft + moveRight, 0.0f);
    }

    // Updates player position with boundary checks
    public void Move() {
        var dynamicShape = Shape.AsDynamicShape();
        float newX = dynamicShape.Position.X + dynamicShape.Velocity.X;
        
        // Prevents moving outside screen
        newX = Math.Clamp(newX, 0.0f, 1.0f - dynamicShape.Extent.X);
        dynamicShape.Position = new Vector2(newX, dynamicShape.Position.Y);
    }

    // Allows the player to shoot bullets
    public void Shoot(EntityContainer<PlayerShot> shots, IBaseImage shotImage) {
        var shotPosition = new Vector2(Shape.Position.X + (Shape.Extent.X / 2) - 0.004f, 
                                    Shape.Position.Y + Shape.Extent.Y);
        
        var shot = new PlayerShot(shotPosition, shotImage);

        shots.AddEntity(shot);
    }





    // Processes keyboard input
    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                SetMoveLeft(action == KeyboardAction.KeyPress);
                break;
            case KeyboardKey.Right:
                SetMoveRight(action == KeyboardAction.KeyPress);
                break;
        }
    }
}