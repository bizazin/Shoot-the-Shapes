using ObjectPooling.Objects;
using UnityEngine;

namespace Signals
{
    public class SignalBulletHitShape
    {
        public ShapeComponentBehaviour HitShapeComponent { get; }
        public Vector3 HitPosition { get; }

        public SignalBulletHitShape(ShapeComponentBehaviour hitShapeComponent, Vector3 hitPosition)
        {
            HitShapeComponent = hitShapeComponent;
            HitPosition = hitPosition;
        }
    }
}