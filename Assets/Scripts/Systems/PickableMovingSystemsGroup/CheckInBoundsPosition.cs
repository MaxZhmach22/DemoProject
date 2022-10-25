using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class CheckInBoundsPosition :  IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsPoolInject<ActivatorTimeComponent> _timePool = default;
        private readonly EcsPoolInject<MovingComponent> _movingCompPool = default;
        private readonly EcsPoolInject<InPoolMarker> _poolMarkerPool = default;
        private readonly EcsFilterInject<Inc<PickableComponent, ActivatorTimeComponent, MovingComponent>, Exc<CatchedComponent>> _filter = default;
        private readonly Camera _camera = Camera.main;
        private readonly int _screenOffset = 150;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                CheckPickablePosition(ref _pickablePool.Value.Get(entity), entity);
            }
        }

        private void CheckPickablePosition(ref PickableComponent pickableComp, int entity)
        {
            var screenPosition = _camera.WorldToScreenPoint(pickableComp.Pickable.transform.position);
            var outOfScreenWidth = screenPosition.x < 0 - _screenOffset || screenPosition.x > Screen.width + _screenOffset;
            var outOfScreenHeight = screenPosition.y < 0 - _screenOffset || screenPosition.y > Screen.height + _screenOffset;
            
            if (outOfScreenWidth || outOfScreenHeight)
            {
                ref var timeActiveComp = ref _timePool.Value.Get(entity);
                pickableComp.Pickable.gameObject.SetActive(false);
                timeActiveComp.CurrentTimeValue = timeActiveComp.StartTimeValue;
                _movingCompPool.Value.Del(entity);
                _poolMarkerPool.Value.Add(entity);
            }
        }
    }
}