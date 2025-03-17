using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga.Squadron {
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
