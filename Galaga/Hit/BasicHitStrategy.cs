namespace Galaga.Hit {
    public class BasicHitStrategy : IHitStrategy {
        public void Hit(Enemy enemy) { // 🚀 Skiftet fra HandleHit til Hit
            enemy.DeleteEntity();
        }
    }
}
