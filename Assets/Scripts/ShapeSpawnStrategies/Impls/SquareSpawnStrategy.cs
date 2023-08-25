using Enums;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class SquareSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IRandomShapeComponentPool _randomShapeComponentPool;
        
        public int gridSize = 10; 
        
        public override EShapeType ShapeType => EShapeType.Square;

        public SquareSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool
        ) : base(randomShapeComponentPool)
        {
            _randomShapeComponentPool = randomShapeComponentPool;
        }

        public override void Spawn(Transform center)
        {
            for (var x = -gridSize; x < gridSize; x++)
                for (var z = -gridSize; z < gridSize; z++)
                {
                    var position = new Vector3(x + 0.5f, center.position.y, z + 0.5f); 

                    var shapeComponentBehaviour = _randomShapeComponentPool.Spawn(center);
                    shapeComponentBehaviour.transform.position = position;
                }

        }
    }
}