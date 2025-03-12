namespace Galaga.Movement;

public interface IMovementStrategy {
    void Scale(float factor);
    void Move (Enemy enemy);
}