using UnityEngine;
using Zenject;

namespace ObjectPooling.Core
{
    public class Pool<T> : MemoryPool<Transform, T>, IPool<T> where T : MonoBehaviour
    {
        protected override void OnSpawned(T item) => item.gameObject.SetActive(true);

        protected override void OnDespawned(T item) => item.gameObject.SetActive(false);
    }
}