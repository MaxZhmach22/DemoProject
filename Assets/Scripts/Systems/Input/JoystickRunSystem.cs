using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace DemoProject.Input
{
    public class JoystickRunSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<JoysticInputComponent> _pool = default;
        private readonly EcsFilterInject<Inc<JoysticInputComponent>> _filter = default;
        private readonly DynamicJoystick _dynamicJoystick;

        public JoystickRunSystem(DynamicJoystick dynamicJoystick)
        {
            _dynamicJoystick = dynamicJoystick;
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var joystickComp = ref _pool.Value.Get(entity);
                joystickComp.Vertical = _dynamicJoystick.Vertical;
                joystickComp.Horizontal = _dynamicJoystick.Horizontal;
                
                Debug.Log($"{joystickComp.Vertical} {joystickComp.Horizontal}");
            }
        }
    }
}