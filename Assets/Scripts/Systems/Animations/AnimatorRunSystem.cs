using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class AnimatorRunSystem : IEcsRunSystem
    {
        private readonly int _blend = Animator.StringToHash("Blend");
        private readonly int _isCatching = Animator.StringToHash("IsCatching");
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<JoysticInputComponent> _joystickPool = default;
        private readonly EcsPoolInject<AnimatorComponent> _animPool = default;
        private readonly EcsPoolInject<HookAnimationRequest> _hookRequestPool = default;
        private readonly EcsFilterInject<Inc<JoysticInputComponent, AnimatorComponent>> _filter = default;
        private readonly EcsFilterInject<Inc<HookAnimationRequest>> _hookRequestFilter = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                SetValue(ref _joystickPool.Value.Get(entity), ref _animPool.Value.Get(entity));
                
                foreach (var hookEntity in _hookRequestFilter.Value)
                {
                    EnableCatchingAnimation(entity);
                    _hookRequestPool.Value.Del(hookEntity);
                }
            }
        }

        private void EnableCatchingAnimation(int entity)
        {
            ref var animatorComp = ref _animPool.Value.Get(entity);
            animatorComp.Animator.SetBool(_isCatching, true);
        }

        private void SetValue(ref JoysticInputComponent joystickComp, ref AnimatorComponent animComp)
        {
            animComp.Animator.SetFloat(_blend, Mathf.Abs(joystickComp.Horizontal) + Mathf.Abs(joystickComp.Vertical));
        }
    }
}