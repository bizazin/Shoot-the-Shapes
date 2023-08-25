using System.Collections.Generic;
using Enums;
using ObjectPooling.Objects;
using UnityEngine;

namespace ShapeSpawnStrategies
{
    public interface IShapeSpawnStrategy
    {
        public EShapeType ShapeType { get; }
        public List<ShapeComponentBehaviour> Spawn(Transform parent, Vector3 spawnPoint);
    }
}