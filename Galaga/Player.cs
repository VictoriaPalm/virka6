namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.Numerics; // Added to access Vector2 5.2.2
using System;          // Added to access Math 5.2.2

public class Player : Entity {
    // Tracks how far left or right the player should move 5.2.2
    private float moveLeft;
    private float moveRight;

    // How fast the player moves 5.2.2
    private const float MOVEMENT_SPEED = 0.01f;

    // Sets up the player with its initial shape and image 5.2.2
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        // Start not moving 5.2.2
        moveLeft = 0.0f;
        moveRight = 0.0f;
    }

    // Decides movement to the left 5.2.2
    public void SetMoveLeft(bool move) {
        // Moves left if true, stops if false 5.2.2
        moveLeft = move ? -MOVEMENT_SPEED : 0.0f;
        UpdateVelocity();
    }

    // Decides movement to the right 5.2.2
    public void SetMoveRight(bool move) {
        // Moves right if true, stops if false 5.2.2
        moveRight = move ? MOVEMENT_SPEED : 0.0f;
        UpdateVelocity();
    }

    // Updates player's speed based on movement direction 5.2.2
    private void UpdateVelocity() {
        // Combines left and right movement into one velocity 5.2.2
        Shape.AsDynamicShape().Velocity = new Vector2(moveLeft + moveRight, 0.0f);
    }

    // Actually moves the player, keeping them on screen 5.2.2
    public void Move() {
        var dynamicShape = Shape.AsDynamicShape();
        float newX = dynamicShape.Position.X + dynamicShape.Velocity.X;
        
        // Keeps player inside the game window 5.2.2
        newX = Math.Clamp(newX, 0.0f, 1.0f - dynamicShape.Extent.X);
        
        dynamicShape.Position = new Vector2(newX, dynamicShape.Position.Y);
    }

    // Handles keyboard input for moving the player 5.2.2
    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                // Start or stop moving left 5.2.2
                SetMoveLeft(action == KeyboardAction.KeyPress);
                break;
            case KeyboardKey.Right:
                // Start or stop moving right 5.2.2
                SetMoveRight(action == KeyboardAction.KeyPress);
                break;
        }
    }
}