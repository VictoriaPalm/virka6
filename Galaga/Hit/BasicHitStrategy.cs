namespace Galaga.Hit {
    public class BasicHitStrategy : IHitStrategy {
        public void Hit(Enemy enemy) { 
            enemy.DeleteEntity();
        }
    }
}
