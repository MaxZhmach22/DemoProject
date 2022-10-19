using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class HookMoveSystem : IEcsRunSystem
    {
        private EcsCustomInject<HookView> _hook = default;
        private EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<HookMoveRequest> _movePool = default;
        private readonly EcsPoolInject<HookAnimationEvent> _animationPool = default;
        private readonly EcsFilterInject<Inc<HookMoveRequest>> _filter = default;
        private readonly EcsFilterInject<Inc<HookAnimationEvent>> _animationEventFilter = default;
        private Vector3 _position;
      

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var hookMoveComp = ref _movePool.Value.Get(entity);
                _position = hookMoveComp.Position;
                _movePool.Value.Del(entity);
            }

            foreach (var entity in _animationEventFilter.Value)
            {
                MoveHook();
                _animationPool.Value.Del(entity);
            }
        }

        private void MoveHook()
        {
            if (_position != Vector3.zero)
            {
                _hook.Value.transform.DOMove(_position, _playerSettings.Value.TimeHookMove)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.Linear)
                    .OnStart(() => KinematikStatus(true))
                    .OnStepComplete(() => Debug.Log("Catch"))
                    .OnComplete(() =>
                    {
                        KinematikStatus(false);
                        _position = Vector3.zero;
                    });
            }
        }

        private void KinematikStatus(bool status)
        {
            _hook.Value.Rigidbody.isKinematic = status;
            _hook.Value.ObiRigidbody.kinematicForParticles = status;
        }
    }
}