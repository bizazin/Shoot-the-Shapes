using ObjectPooling.Objects;
using UnityEngine;

namespace ObjectPooling.Pools
{
    public interface IRandomShapeComponentPool
    {
        ShapeComponentBehaviour Spawn(Transform parent);
        void Despawn(ShapeComponentBehaviour item);
    }
}