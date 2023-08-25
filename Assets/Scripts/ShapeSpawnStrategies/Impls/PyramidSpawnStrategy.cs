using Enums;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class PyramidSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IRandomShapeComponentPool _randomShapeComponentPool;
        
        public int baseSize = 10; // Размер основания пирамиды
        public override EShapeType ShapeType => EShapeType.Pyramid;

        public PyramidSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool
        ) : base(randomShapeComponentPool)
        {
            _randomShapeComponentPool = randomShapeComponentPool;
        }

        public override void Spawn(Transform center)
        {
            for (var y = 0; y < baseSize; y++)
            {
                var currentBaseSize = baseSize - y;
                var halfBaseSize = currentBaseSize / 2;

                for (var x = -halfBaseSize; x < halfBaseSize; x++)
                    for (var z = -halfBaseSize; z < halfBaseSize; z++)
                    {
                        var centerPos = center.position;
                        var position = new Vector3(x + centerPos.x, y + centerPos.y, z + centerPos.z);

                        var shapeComponentBehaviour = _randomShapeComponentPool.Spawn(center);
                        shapeComponentBehaviour.transform.position = position;
                    }
            }

        }
    }
}