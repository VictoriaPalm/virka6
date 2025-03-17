using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga.Squadron {
    public class VFormationSquadron : SquadronBase {
        public override List<Vector2> GetFormationPositions() {
            float xStart = 0.5f;
            float yStart = 0.8f;
            return new List<Vector2> {
                new Vector2(xStart, yStart),
                new Vector2(xStart - 0.12f, yStart - 0.07f), 
                new Vector2(xStart + 0.12f, yStart - 0.07f),
                new Vector2(xStart - 0.22f, yStart - 0.14f),
                new Vector2(xStart + 0.22f, yStart - 0.14f)
            };
        }
    }
}
