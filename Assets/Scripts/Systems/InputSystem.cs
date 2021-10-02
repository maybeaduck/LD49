using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class InputSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {

            var rotationX = Input.GetAxisRaw("Mouse X");
            var rotationY = Input.GetAxisRaw("Mouse Y");
            _runtimeData.RotationX = rotationX;
            _runtimeData.RotationY = rotationY;
            _runtimeData.RotationXRaw = rotationX * _staticData.MouseSensitivity;
            _runtimeData.RotationYRaw = rotationY * _staticData.MouseSensitivity;

            var verticalButton = Input.GetAxisRaw("Vertical");
            var horizontalButton = Input.GetAxisRaw("Horizontal");
            _runtimeData.Direction = new Vector2(horizontalButton, verticalButton);
        }
    }
}