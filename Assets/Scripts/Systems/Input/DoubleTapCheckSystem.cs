using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UniRx;
using UnityEngine;

namespace DemoProject
{
    public class DoubleTapCheckSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<DoubleClickRequest> _pool = default;
        private readonly CompositeDisposable _disposable;

        public DoubleTapCheckSystem(CompositeDisposable disposable)
        {
            _disposable = disposable;
        }
        
        public void Init(IEcsSystems systems)
        {
            var clicks= Observable.EveryUpdate()
                .Where(_ => UnityEngine.Input.GetMouseButtonDown(0));

            clicks.Buffer(clicks.Throttle(TimeSpan.FromMilliseconds(250)))
                .Where(x => x.Count >= 2)
                .Subscribe(_ => DoubleClicked())
                .AddTo(_disposable);
        }

        private void DoubleClicked()
        {
            ref var request = ref _pool.Value.Add(_world.Value.NewEntity());
            request.Position = UnityEngine.Input.mousePosition;
            Debug.Log("Clicked!");
        }
    }
}