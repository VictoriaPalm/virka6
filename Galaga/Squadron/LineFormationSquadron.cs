using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga.Squadron {
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
}
