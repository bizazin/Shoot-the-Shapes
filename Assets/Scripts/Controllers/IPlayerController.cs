using UnityEngine;

namespace Controllers
{
    public interface IPlayerController
    {
        void TurnInDirection(Vector2 moveVector);
    }
}