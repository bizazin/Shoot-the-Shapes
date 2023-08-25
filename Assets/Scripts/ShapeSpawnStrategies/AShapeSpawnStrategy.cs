using Enums;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies
{
    public abstract class AShapeSpawnStrategy : IShapeSpawnStrategy
    {
        public abstract EShapeType ShapeType { get; }

        protected AShapeSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool
        )
        {
        }

        public abstract void Spawn(Transform center);
    }
}