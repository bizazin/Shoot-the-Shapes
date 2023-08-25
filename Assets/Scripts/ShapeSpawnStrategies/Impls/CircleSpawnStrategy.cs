using Enums;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class CircleSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IRandomShapeComponentPool _randomShapeComponentPool;
        public int gridSize = 10;
        public float radius = 5f;

        public override EShapeType ShapeType => EShapeType.Circle;

        public CircleSpawnStrategy
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

                    if (Vector3.Distance(center.position, position) <= radius)
                    {
                        var shapeComponentBehaviour = _randomShapeComponentPool.Spawn(center);
                        shapeComponentBehaviour.transform.position = position;
                    }
                }
        }
    }
}