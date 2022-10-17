using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace DemoProject.Player
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<PlayerMarkerComponent> _pool = default;
        private readonly PlayerView _playerView;

        public PlayerInitSystem(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public void Init(IEcsSystems systems)
        {
            if (_playerView)
            {
                _pool.Value.Add(_world.Value.NewEntity());
            }
        }
    }
}