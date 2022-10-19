using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace DemoProject
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<PlayerTransformComponent> _pool = default;
        private readonly EcsFilterInject<Inc<JoysticInputComponent>> _joystickFilter = default;
        private readonly PlayerView _playerView;

        public PlayerInitSystem(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public void Init(IEcsSystems systems)
        {
            if (_playerView)
            {
                foreach (var entity in _joystickFilter.Value)
                {
                    ref var playerComp = ref _pool.Value.Add(entity);
                    playerComp.Transform = _playerView.transform;
                }
            }
        }
    }
}