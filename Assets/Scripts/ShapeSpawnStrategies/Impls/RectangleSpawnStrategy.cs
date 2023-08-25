using System.Collections.Generic;
using Databases;
using Enums;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class RectangleSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;
        
        public override EShapeType ShapeType => EShapeType.Rectangle;

        public RectangleSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool,
            IShapeSettingsDatabase shapeSettingsDatabase,
            IColorSettingsDatabase colorSettingsDatabase
        ) : base(randomShapeComponentPool, shapeSettingsDatabase, colorSettingsDatabase)
        {
            _shapeSettingsDatabase = shapeSettingsDatabase;
        }

        public override List<ShapeComponentBehaviour> Spawn(Transform parent, Vector3 spawnPoint)
        {
            var halfWidth = _shapeSettingsDatabase.RectangleSettings.Width / 2;
            var halfHeight = _shapeSettingsDatabase.RectangleSettings.Height / 2;

            CurrentShapeComponents.Clear();
            for (var x = -halfWidth; x < halfWidth; x++)
                for (var y = -halfHeight; y < halfHeight; y++)
                {
                    var position = new Vector3(spawnPoint.x + x, spawnPoint.y + y - halfHeight, spawnPoint.z); 
                    SpawnAndAddComponent(parent, position);
                }

            return CurrentShapeComponents;
        }
    }
}