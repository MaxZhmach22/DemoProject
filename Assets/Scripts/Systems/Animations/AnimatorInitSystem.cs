using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace DemoProject
{
    public class AnimatorInitSystem : IEcsInitSystem
    {
        private readonly PlayerAnimatorView _animator;
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<AnimatorComponent> _pool = default;
        private readonly EcsFilterInject<Inc<JoysticInputComponent>> _filter = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent>> _playerFilter = default;

        public AnimatorInitSystem(PlayerAnimatorView playerAnimatorView)
        {
            _animator = playerAnimatorView;
        }

        public void Init(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                _animator.InitEcsWorld(_world.Value, playerEntity);
            }
           
            
            foreach (var entity in _filter.Value)
            {
                SetAnimator(ref _pool.Value.Add(entity));
            }
        }

        private void SetAnimator(ref AnimatorComponent animComp)
        {
            animComp.Animator = _animator.Animator;
        }
    }
}