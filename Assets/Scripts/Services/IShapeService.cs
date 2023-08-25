using ObjectPooling.Objects;
using UnityEngine;

namespace Services
{
    public interface IShapeService
    {
        Vector3 SpawnShape(Transform player);
        void DestroyCurrentShape(ShapeComponentBehaviour shapeComponent, Vector3 hitPosition);
        void DespawnCurrentShape();
    }
}