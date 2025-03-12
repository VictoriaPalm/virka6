namespace Galaga.Squadron;


using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System; 
using Galaga.Movement;
using Galaga.Hit;

public interface ISquadron {
    EntityContainer<Enemy> CreateEnemies(
    List<Image> enemyStrides, 
    List<Image> enragedStrides,
    Func<IMovementStrategy> movement,
    Func<IHitStrategy> hit

    );
}