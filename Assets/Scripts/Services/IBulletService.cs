using ObjectPooling.Objects;
using UnityEngine;

namespace Services
{
    public interface IBulletService
    {
        BulletBehaviour SpawnBullet(Transform spawnTransform);
    }
}