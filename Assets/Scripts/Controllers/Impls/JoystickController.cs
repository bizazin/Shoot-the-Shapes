using System;
using Core.Abstracts;
using SimpleInputNamespace;
using UniRx;
using UnityEngine;
using Zenject;

namespace Controllers.Impls
{
    public class JoystickController : Controller<Joystick>, IJoystickController, IInitializable
    {
        private readonly IPlayerController _playerController;

        public JoystickController
        (
            IPlayerController playerController
        )
        {
            _playerController = playerController;
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
                .Select(_ => new Vector2(View.xAxis.value, View.yAxis.value))
                .Subscribe(moveVector => _playerController.TurnInDirection(moveVector))
                .AddTo(View);
        }
    }
}