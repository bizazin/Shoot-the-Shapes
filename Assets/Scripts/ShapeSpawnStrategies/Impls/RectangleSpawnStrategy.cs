using Enums;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class RectangleSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IRandomShapeComponentPool _randomShapeComponentPool;
        public int width = 10; // Ширина прямоугольника
        public int height = 5; // Высота прямоугольника
        public override EShapeType ShapeType => EShapeType.Rectangle;

        public RectangleSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool
        ) : base(randomShapeComponentPool)
        {
            _randomShapeComponentPool = randomShapeComponentPool;
        }

        public override void Spawn(Transform center)
        {
            int halfWidth = width / 2;
            int halfHeight = height / 2;

            for (var x = -halfWidth; x < halfWidth; x++)
                for (var z = -halfHeight; z < halfHeight; z++)
                {
                    var position = new Vector3(x + 0.5f, center.position.y, z + 0.5f); 

                    var shapeComponentBehaviour = _randomShapeComponentPool.Spawn(center);
                    shapeComponentBehaviour.transform.position = position;
                }

        }
    }
}