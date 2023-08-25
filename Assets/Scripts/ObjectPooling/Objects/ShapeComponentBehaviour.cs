using Enums;
using UnityEngine;

namespace ObjectPooling.Objects
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class ShapeComponentBehaviour : MonoBehaviour
    {
        [SerializeField] private EShapeComponentType _shapeType;

        public EShapeComponentType ShapeType => _shapeType;
    }
}