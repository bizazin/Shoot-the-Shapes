using Enums;
using ObjectPooling.Objects;
using UnityEngine;

namespace ObjectPooling.Pools
{
    public interface IShapeComponentPool
    {
        EShapeComponentType ShapeType { get; }
        ShapeComponentBehaviour Spawn(Transform parent);
        void Despawn(ShapeComponentBehaviour item);
    }
}