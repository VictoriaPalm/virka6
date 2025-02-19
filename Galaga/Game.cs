namespace Galaga;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
public class Game : DIKUGame {
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // TODO
    }

    public override void Render(WindowContext context) {
        throw new System.NotImplementedException("Galaga game has nothing to render yet.");
    }

    public override void Update() {
        throw new System.NotImplementedException("Galaga game has no entities to update yet.");
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        throw new System.NotImplementedException("Galaga game has no entities to update yet.");
    }

}
