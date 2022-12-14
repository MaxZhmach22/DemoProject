using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class DoubleClickRaycastSystem : IEcsRunSystem
    {
        private readonly Camera _camera = Camera.main;
        private readonly RaycastHit[] _hits = new RaycastHit[5];
        private readonly EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<DoubleClickRequest> _clickRequest = default;
        private readonly EcsPoolInject<HookAnimationRequest> _animationRequestPool = default;
        private readonly EcsPoolInject<HookMoveRequest> _hookMovePool = default;
        private readonly EcsPoolInject<LookOnPickableRequest> _lookOnPickablePool = default;
        private readonly EcsFilterInject<Inc<DoubleClickRequest>> _doubleClickFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent>> _playerFilter = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _doubleClickFilter.Value)
            {
                _animationRequestPool.Value.Add(_world.Value.NewEntity());
                
                ref var clickComp = ref _clickRequest.Value.Get(entity);
                var ray = _camera.ScreenPointToRay(clickComp.Position);
                
                var hits = Physics.RaycastNonAlloc(ray, _hits, 300, _playerSettings.Value.DoubleClickRaycast);
                
                RayCasting(hits);
                
                _clickRequest.Value.Del(entity);
            }
        }

        private void RayCasting(int hits)
        {
            if (hits != 0)
            {
                foreach (var hitInfo in _hits)
                {
                    if (hitInfo.collider == null)
                    {
                        return;
                    }

                    if (hitInfo.collider.TryGetComponent<PickableView>(out var pickableView))
                    {
                        Debug.Log(pickableView.name);
                    }
                    
                    ref var hookMoveComp = ref _hookMovePool.Value.Add(_world.Value.NewEntity());
                    hookMoveComp.Position = hitInfo.point;
                    DebugExtension.DebugCapsule(hitInfo.point, hitInfo.point + Vector3.up, Color.cyan, 1f, 5f);

                    foreach (var playerEntity in _playerFilter.Value)
                    {
                        if (!_lookOnPickablePool.Value.Has(playerEntity))
                        {
                            ref var lookOnComp = ref _lookOnPickablePool.Value.Add(playerEntity);
                            lookOnComp.PositionToLook = hitInfo.point;
                        }
                    }
                }
            }
        }
    }
}