using System.Collections.Generic;
using System.Linq;
using Enums;
using Extensions;
using ObjectPooling.Objects;
using UnityEngine;

namespace ObjectPooling.Pools.Impls
{
    public class RandomShapeComponentPool : IRandomShapeComponentPool
    {
        private readonly Dictionary<EShapeComponentType, IShapeComponentPool> _shapeComponentPools;

        public RandomShapeComponentPool
        (
            List<IShapeComponentPool> shapeComponentPools
        )
        {
            _shapeComponentPools = shapeComponentPools.ToDictionary(pool => pool.ShapeType);
        }

        public ShapeComponentBehaviour Spawn(Transform parent) =>
            _shapeComponentPools[EnumExtensions.GetRandomValue<EShapeComponentType>()].Spawn(parent);

        public void Despawn(ShapeComponentBehaviour item) => _shapeComponentPools[item.ShapeType].Despawn(item);
    }
}