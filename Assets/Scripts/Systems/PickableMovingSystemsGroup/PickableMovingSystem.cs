using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class PickableMovingSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsPoolInject<MovingComponent> _movingCompPool = default;
        private readonly EcsPoolInject<FindDirectionRequest> _directionReauestPool = default;
        private readonly EcsFilterInject<Inc<PickableComponent, MovingComponent>, Exc<FindDirectionRequest, CatchedComponent>> _filter = default;
     
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var pickable = ref _pickablePool.Value.Get(entity);
                ref var movingComp = ref _movingCompPool.Value.Get(entity);

                if (movingComp.TimeCounter > 0)
                {
                    SetRotation(movingComp, pickable);
                    movingComp.TimeCounter -= Time.deltaTime;
                    pickable.Pickable.transform.position += movingComp.DirectionToMove * Time.deltaTime * movingComp.MovingSpeedRange;
                    DebugExtension.DebugArrow(pickable.Pickable.transform.position, movingComp.DirectionToMove * 8, Color.cyan);
                }
                else
                {
                    _directionReauestPool.Value.Add(entity);
                    _movingCompPool.Value.Del(entity);
                }
            }
        }

        private void SetRotation(MovingComponent movingComp, PickableComponent pickable)
        {
            if(movingComp.IsSet) return;

            pickable.Pickable.transform.DOLookAt(movingComp.DirectionToMove + pickable.Pickable.transform.position, 1f)
                .OnStart(() => movingComp.IsSet = true);
        }
    }
}