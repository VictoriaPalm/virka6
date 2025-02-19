namespace GalagaTests;

using NUnit.Framework;
using Galaga;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class TestsPlayer {
    public Player testPlayer;
    public Player testPlayer2;

    [SetUp]
    public void Setup() {
        testPlayer = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)),
            new Image("GalagaTests.Images.Player.png"));
        testPlayer2 = new Player(
            new DynamicShape(new Vector2(0.99f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage());
    }

    [Test]
    public void Test1() {
        Assert.AreEqual(1, 1);
    }

    // TODO: Your tests
}