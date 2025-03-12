using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
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
            
            if (enemyStrides == null || enragedStrides == null || movementFactory == null || hitFactory == null) {
                throw new ArgumentNullException("CreateEnemies received a null argument.");
            }
            var enemies = new EntityContainer<Enemy>();
            foreach (var position in GetFormationPositions()) {
            var enemy = new Enemy (
                new DynamicShape(position, new Vector2(0.1f, 0.1f)),
                enemyStrides[0] 
                );

                enemies.AddEntity(enemy);
            }
            return enemies;
        }
    }
    
    public class VFormationSquadron : SquadronBase {
        public override List<Vector2> GetFormationPositions() {
            float xStart = 0.5f;
            float yStart = 0.8f;
            return new List<Vector2> {
                new Vector2(xStart, yStart),
                new Vector2(xStart - 0.05f, yStart - 0.05f),
                new Vector2(xStart + 0.05f, yStart - 0.05f),
                new Vector2(xStart - 0.1f, yStart - 0.1f),
                new Vector2(xStart + 0.1f, yStart - 0.1f)
            };
        }
    }
    
    public class LineFormationSquadron : SquadronBase {
        public override List<Vector2> GetFormationPositions() {
            float xStart = 0.2f;
            float yStart = 0.7f;
            var positions = new List<Vector2>();
            for (int i = 0; i < 6; i++) {
                positions.Add(new Vector2(xStart + i * 0.12f, yStart));
            }
            return positions;
        }
    }
    
    public class CircleFormationSquadron : SquadronBase {
        public override List<Vector2> GetFormationPositions() {
            float centerX = 0.5f;
            float centerY = 0.75f;
            float radius = 0.15f;
            int numEnemies = 8;
            var positions = new List<Vector2>();
            for (int i = 0; i < numEnemies; i++) {
                float angle = (float)(2 * Math.PI * i / numEnemies);
                positions.Add(new Vector2(
                    centerX + radius * (float)Math.Cos(angle),
                    centerY + radius * (float)Math.Sin(angle)
                ));
            }
            return positions;
        }
    }
}
