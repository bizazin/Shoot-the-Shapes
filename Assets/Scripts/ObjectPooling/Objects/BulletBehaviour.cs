using System;
using UnityEngine;

namespace ObjectPooling.Objects
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        public event Action OnShapeHit;
        public Action CurrentHitAction { get; set; }


        public void Launch(float bulletVelocity, Vector3 barrelDirection) =>
            _rigidbody.velocity = barrelDirection * bulletVelocity;

        private void OnCollisionEnter(Collision other)
        {
            var shapeComponent = other.gameObject.GetComponent<ShapeComponentBehaviour>();
            if (shapeComponent != null) 
                OnShapeHit?.Invoke();
        }
    }
}