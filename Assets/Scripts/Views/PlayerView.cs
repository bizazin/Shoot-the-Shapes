using Core.Abstracts;
using UnityEngine;

namespace Views
{
    public class PlayerView : View
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _bulletSpawnTransform;

        public Transform BulletSpawnTransform => _bulletSpawnTransform;
        public Transform Player => _player;
    }
}