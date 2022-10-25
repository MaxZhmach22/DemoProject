using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DemoProject
{
    public class FindDirectionSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<MovingComponent> _movingPool = default;
        private readonly EcsPoolInject<FindDirectionRequest> _requestPool = default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsFilterInject<Inc<PickableComponent, FindDirectionRequest>> _filter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                SetValue(ref _pickablePool.Value.Get(entity), ref _movingPool.Value.Add(entity));
                _requestPool.Value.Del(entity);
            }
        }

        private void SetValue(ref PickableComponent pickable, ref MovingComponent moving)
        {
            var randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            moving.DirectionToMove = randomDirection;
            moving.TimeCounter = Random.Range(5f, 10f);
            moving.MovingSpeedRange = Random.Range(pickable.Pickable.MovingSpeedRange.x, pickable.Pickable.MovingSpeedRange.y);
        }
    }
}