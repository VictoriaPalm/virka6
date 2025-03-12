namespace Galaga.Movement {
    public class NoMoveStrategy : IMovementStrategy {
        public void Move(Enemy enemy) {
            // Ingen bevægelse
        }

        public void Scale(float factor) {
            // Ingen skalering
        }
    }
}
