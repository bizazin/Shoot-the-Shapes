using Enums;
using UnityEngine;

namespace ShapeSpawnStrategies
{
    public interface IShapeSpawnStrategy
    {
        public EShapeType ShapeType { get; }
        public void Spawn(Transform center);
    }
}