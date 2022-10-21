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
        private readonly EcsPoolInject<LookOnPickableRequest> _lookOnPockablePool = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent, JoysticInputComponent>, Exc<IsCatchingMarker>> _idleRotationFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent,LookOnPickableRequest>> _catchingRotationFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent, JoysticInputComponent>, Exc<NoGroundMarker, IsCatchingMarker>> _movementFilter = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _idleRotationFilter.Value)
            {
                ref var playerComp = ref _playerTransformPool.Value.Get(entity);
                ref var inputComp = ref _inputPool.Value.Get(entity);

                SetIdleRotation(playerComp, inputComp);
            }

            foreach (var entity in _movementFilter.Value)
            {
                ref var playerComp = ref _playerTransformPool.Value.Get(entity);
                ref var inputComp = ref _inputPool.Value.Get(entity);

                Move(playerComp, inputComp);
            }

            foreach (var entity in _catchingRotationFilter.Value)
            {
                ref var playerComp = ref _playerTransformPool.Value.Get(entity);
                ref var lookComp = ref _lookOnPockablePool.Value.Get(entity);

                SetCatchRotation(lookComp, playerComp);
                _lookOnPockablePool.Value.Del(entity);

            }
        }

        private void Move(PlayerTransformComponent playerComp, JoysticInputComponent inputComp)
        {
            var directionToMove = new Vector3(inputComp.Horizontal, 0, inputComp.Vertical) *
                                  _playerSettings.Value.WalkSpeed * Time.deltaTime;

            playerComp.Transform.position += directionToMove;
            playerComp.Transform.position = new Vector3(playerComp.Transform.position.x, 0, playerComp.Transform.position.z);
            
        }

        private void SetIdleRotation(PlayerTransformComponent playerComp, JoysticInputComponent inputComp)
        {
            var directionLookAt = playerComp.Transform.position +
                                  new Vector3(inputComp.Horizontal, 0, inputComp.Vertical);
            
            playerComp.Transform.LookAt(directionLookAt);
        }

        private static void SetCatchRotation(LookOnPickableRequest lookComp, PlayerTransformComponent playerComp)
        {
            var directionTolook = new Vector3(lookComp.PositionToLook.x, playerComp.Transform.position.y,
                lookComp.PositionToLook.z);
            playerComp.Transform.LookAt(directionTolook);
            DebugExtension.DebugArrow(playerComp.Transform.position, directionTolook, Color.red, 5f);
        }
    }
}