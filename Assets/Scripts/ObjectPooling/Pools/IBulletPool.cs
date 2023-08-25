using ObjectPooling.Core;
using ObjectPooling.Objects;
using UnityEngine;

namespace ObjectPooling.Pools
{
    public interface IBulletPool : IPool<BulletBehaviour>
    {
        // BulletBehaviour SpawnAndActivate(Transform parent);
        //void DespawnAndDeactivate(BulletBehaviour bullet);
    }
}