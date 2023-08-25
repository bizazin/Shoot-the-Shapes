using Core.Abstracts;
using UnityEngine;

namespace Views
{
    public class PlayerView : View
    {
        [SerializeField] private Transform _bulletSpawnTransform;
        [SerializeField] private Transform _riflePivotTransform;

        public Transform BulletSpawnTransform => _bulletSpawnTransform;
        public Transform RiflePivotTransform => _riflePivotTransform;
    }
}