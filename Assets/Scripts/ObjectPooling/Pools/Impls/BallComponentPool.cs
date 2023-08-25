using Enums;
using ObjectPooling.Core;
using ObjectPooling.Objects;

namespace ObjectPooling.Pools.Impls
{
    public class BallComponentPool : Pool<ShapeComponentBehaviour>, IShapeComponentPool, IBallComponentPool
    {
        public EShapeComponentType ShapeType => EShapeComponentType.Ball;
    }
}