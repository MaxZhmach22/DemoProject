using System;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace DemoProject
{
    public class IcelandSwingSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<UnitTransformComponent> _pool = default;
        private readonly Transform _icelandTransform;

        public IcelandSwingSystem(Transform icelandTransform)
        {
            _icelandTransform = icelandTransform;
        }

        public void Init(IEcsSystems systems)
        {
            ref var unitTransform = ref _pool.Value.Add(_world.Value.NewEntity());
            unitTransform.Transform = _icelandTransform;

            Swing();
        }

        private void Swing()
        {
            _icelandTransform.transform.DOLocalRotate(new Vector3(-3, 0, 0), 2)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}