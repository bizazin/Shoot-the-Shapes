using Enums;
using UnityEngine;

namespace ObjectPooling.Objects
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
    public class ShapeComponentBehaviour : MonoBehaviour
    {
        [SerializeField] private EShapeComponentType _shapeType;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Renderer _renderer;

        public EShapeComponentType ShapeType => _shapeType;
        public Rigidbody Rigidbody => _rigidbody;
        public Renderer Renderer => _renderer;
    }
}