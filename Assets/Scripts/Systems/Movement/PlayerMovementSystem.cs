using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<JoysticInputComponent> _inputPool = default;
        private readonly EcsPoolInject<PlayerTransformComponent> _playerTransformPool = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent, JoysticInputComponent>> _rotationFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent, JoysticInputComponent>, Exc<NoGroundMarker>> _movementFilter = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _rotationFilter.Value)
            {
                ref var playerComp = ref _playerTransformPool.Value.Get(entity);
                ref var inputComp = ref _inputPool.Value.Get(entity);

                SetRotation(playerComp, inputComp);
            }

            foreach (var entity in _movementFilter.Value)
            {
                ref var playerComp = ref _playerTransformPool.Value.Get(entity);
                ref var inputComp = ref _inputPool.Value.Get(entity);

                Move(playerComp, inputComp);
            }
        }

        private void Move(PlayerTransformComponent playerComp, JoysticInputComponent inputComp)
        {
            var directionToMove = new Vector3(inputComp.Horizontal, 0, inputComp.Vertical) *
                                  _playerSettings.Value.WalkSpeed * Time.deltaTime;

            playerComp.Transform.position += directionToMove;
            playerComp.Transform.position = new Vector3(playerComp.Transform.position.x, 0, playerComp.Transform.position.z);
            
        }

        private void SetRotation(PlayerTransformComponent playerComp, JoysticInputComponent inputComp)
        {
            var directionLookAt = playerComp.Transform.position +
                                  new Vector3(inputComp.Horizontal, 0, inputComp.Vertical);
            
            playerComp.Transform.LookAt(directionLookAt);
        }
    }
}