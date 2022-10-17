using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace DemoProject.Input
{
    public class JoystickInputInit : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<JoysticInputComponent> _pool = default;
        private readonly DynamicJoystick _dynamicJoystick;

        public JoystickInputInit(DynamicJoystick dynamicJoystick)
        {
            _dynamicJoystick = dynamicJoystick;
        }

        public void Init(IEcsSystems systems)
        {
            if (_dynamicJoystick)
            {
                _pool.Value.Add(_world.Value.NewEntity());
            }
        }
    }
}