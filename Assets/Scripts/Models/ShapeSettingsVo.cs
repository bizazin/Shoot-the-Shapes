using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ShapeSettingsVo
    {
        public Vector2Int SpawnRadius;
        public Vector2Int VerticalViewAngleDeg;
        public float HitForce;
        public float SpawnIntervalS;
        public float StandardComponentSize;
    }
}