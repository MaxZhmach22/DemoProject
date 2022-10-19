using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace DemoProject
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<JoysticInputComponent> _inputPool = default;
        private readonly EcsPoolInject<PlayerTransformComponent> _playerTransformPool = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent, JoysticInputComponent>> _filter = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var playerComp = ref _playerTransformPool.Value.Get(entity);
                ref var inputComp = ref _inputPool.Value.Get(entity);

                SetRotation(playerComp, inputComp);
            }
        }

        private void SetRotation(PlayerTransformComponent playerComp, JoysticInputComponent inputComp)
        {
            var directionLookAt = playerComp.Transform.position +
                                  new Vector3(inputComp.Horizontal, 0, inputComp.Vertical);
            
            playerComp.Transform.LookAt(directionLookAt);
        }
    }
}