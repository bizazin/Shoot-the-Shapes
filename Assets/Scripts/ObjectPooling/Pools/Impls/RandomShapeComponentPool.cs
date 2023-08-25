using System.Collections.Generic;
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
            ICubeComponentPool cubeComponentPool,
            IBallComponentPool ballComponentPool
        )
        {
            _shapeComponentPools = new Dictionary<EShapeComponentType, IShapeComponentPool>
            {
                { ballComponentPool.ShapeType, ballComponentPool },
                { cubeComponentPool.ShapeType, cubeComponentPool }
            };
        }

        public ShapeComponentBehaviour Spawn(Transform parent) =>
            _shapeComponentPools[EnumExtensions.GetRandomValue<EShapeComponentType>()].Spawn(parent);

        public void Despawn(ShapeComponentBehaviour item) => _shapeComponentPools[item.ShapeType].Despawn(item);
    }
}