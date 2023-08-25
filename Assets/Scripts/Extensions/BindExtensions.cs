using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Extensions
{
    public static class BindExtensions
    {
        public static void BindView<T, TU>(this DiContainer container, Object viewPrefab, Transform parent)
            where TU : IView
            where T : IController
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
            container.BindInterfacesAndSelfTo<TU>()
                .FromComponentInNewPrefab(viewPrefab)
                .UnderTransform(parent).AsSingle();
        }
        
        public static void BindView<T, TU>(this DiContainer container, Object viewPrefab)
            where TU : IView
            where T : IController
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
            container.BindInterfacesAndSelfTo<TU>()
                .FromComponentInNewPrefab(viewPrefab)
                .AsSingle();
        }
    }
}