using System;
using UnityEngine;

namespace ObjectPooling.Objects
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Renderer _renderer;
        public event Action<ShapeComponentBehaviour, Vector3> OnShapeHit;
        public Action<ShapeComponentBehaviour, Vector3> CurrentHitAction { get; set; }
        public Renderer Renderer => _renderer;


        public void Launch(float bulletVelocity, Vector3 barrelDirection) =>
            _rigidbody.velocity = barrelDirection * bulletVelocity;

        private void OnCollisionEnter(Collision other)
        {
            var shapeComponent = other.gameObject.GetComponent<ShapeComponentBehaviour>();
            if (shapeComponent != null && other.contacts.Length > 0)
                OnShapeHit?.Invoke(shapeComponent, other.contacts[0].point);
        }
    }
}