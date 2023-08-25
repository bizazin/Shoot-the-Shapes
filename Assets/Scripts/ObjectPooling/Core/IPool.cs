using UnityEngine;
using Zenject;

namespace ObjectPooling.Core
{
    public interface IPool<T> : IMemoryPool<Transform, T> where T : MonoBehaviour
    {
    }
}