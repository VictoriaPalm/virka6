namespace GalagaTests;

using NUnit.Framework;
using Galaga;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Linq;

public class TestsPlayer {
    private Player testPlayer = null!;
    private Player testPlayerAtRightBoundary = null!;

    [SetUp]
    public void Setup() {
        testPlayer = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage()
        );

        testPlayerAtRightBoundary = new Player(
            new DynamicShape(new Vector2(0.99f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage()
        );
    }

    [Test]
    public void Test_PlayerMovesLeft() {
        testPlayer.SetMoveLeft(true);
        testPlayer.Move();
        Assert.That(testPlayer.GetPosition().X, Is.LessThan(0.45f));
    }

    [Test]
    public void Test_PlayerMovesRight() {
        testPlayer.SetMoveRight(true);
        testPlayer.Move();
        Assert.That(testPlayer.GetPosition().X, Is.GreaterThan(0.45f));
    }

    [Test]
    public void Test_PlayerStopsMoving() {
        testPlayer.SetMoveLeft(false);
        testPlayer.SetMoveRight(false);
        testPlayer.Move();
        Assert.That(testPlayer.GetPosition().X, Is.EqualTo(0.45f));
    }

    [Test]
    public void Test_PlayerDoesNotMovePastLeftBoundary() {
        testPlayer.Shape.Position = new Vector2(0.0f, testPlayer.Shape.Position.Y);
        testPlayer.SetMoveLeft(true);
        testPlayer.Move();
        Assert.That(testPlayer.GetPosition().X, Is.EqualTo(0.0f));
    }

    [Test]
    public void Test_PlayerDoesNotMovePastRightBoundary() {
        testPlayerAtRightBoundary.SetMoveRight(true);
        testPlayerAtRightBoundary.Move();
        Assert.That(testPlayerAtRightBoundary.GetPosition().X, Is.LessThanOrEqualTo(1.0f));
    }

    [Test]
    public void Test_PlayerPositionMethodWorks() {
        Vector2 position = testPlayer.GetPosition();
        Assert.That(position, Is.EqualTo(new Vector2(0.45f, 0.1f)));
    }
}
