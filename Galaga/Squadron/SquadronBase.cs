using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Galaga.Movement;
using Galaga.Hit;

namespace Galaga.Squadron {
    public abstract class SquadronBase : ISquadron {
        public abstract List<Vector2> GetFormationPositions();

        public EntityContainer<Enemy> CreateEnemies(
            List<Image> enemyStrides, 
            List<Image> enragedStrides,
            Func<IMovementStrategy> movementFactory,
            Func<IHitStrategy> hitFactory) {

            var enemies = new EntityContainer<Enemy>();

            foreach (var position in GetFormationPositions()) {
                var enemy = new Enemy(
                    new DynamicShape(position, new Vector2(0.1f, 0.1f)),
                    enemyStrides[0]
                );

                enemy.SetGameEventBus(Game.GetInstance().GetGameEventBus());

                enemies.AddEntity(enemy);
            }

            return enemies;
        }
    }
}
