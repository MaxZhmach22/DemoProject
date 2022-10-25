using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class PickableActivatorSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsPoolInject<InPoolMarker> _markerPool= default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsPoolInject<ActivatorTimeComponent> _activatopPool = default;
        private readonly EcsPoolInject<FindDirectionRequest> _wayPointPool = default;
        private readonly EcsFilterInject<Inc<PickableComponent, InPoolMarker, ActivatorTimeComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var activator = ref _activatopPool.Value.Get(entity);
                activator.CurrentTimeValue -= Time.deltaTime;
                if (activator.CurrentTimeValue <= 0)
                {
                    ref var pickableComp = ref _pickablePool.Value.Get(entity);
                    SetRandomPosition(pickableComp);
                    DiveOut(pickableComp);
                    _markerPool.Value.Del(entity);
                    _wayPointPool.Value.Add(entity);
                }
            }
        }

        private void SetRandomPosition(PickableComponent pickableComp)
        {
            var bounds = _playerSettings.Value.RespawnZone.bounds;
            
            var upLeftCorner = new Vector3(bounds.min.x, bounds.center.y, bounds.max.z);
            var upRightCorner = new Vector3(bounds.max.x, bounds.center.y, bounds.max.z);
            var downRightCorner = new Vector3(bounds.max.x, bounds.center.y, bounds.min.z);
            var downLeftCorner = new Vector3(bounds.min.x, bounds.center.y, bounds.min.z);

            var position = new Vector3(Random.Range(downLeftCorner.x, downRightCorner.x), -4,
                Random.Range(downRightCorner.z, upRightCorner.z));
            pickableComp.Pickable.gameObject.SetActive(true);
            pickableComp.Pickable.transform.position = position;
        }

        private void DiveOut(PickableComponent pickableComp)
        {
            pickableComp.Pickable.transform.DOLocalMoveY(0, 2);
        }
    }
}