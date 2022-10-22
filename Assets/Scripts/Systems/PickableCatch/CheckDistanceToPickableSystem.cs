using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class CheckDistanceToPickableSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsPoolInject<CheckDistanceToPickableRequest> _requestPool = default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsPoolInject<PickableCatchRequest> _catchedPickablePool = default;
        private readonly EcsFilterInject<Inc<PickableComponent>, Exc<InPoolMarker>> _pickableFilter = default;
        private readonly EcsFilterInject<Inc<CheckDistanceToPickableRequest>> _requestFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var request in _requestFilter.Value)
            {
                ref var requestComp = ref _requestPool.Value.Get(request);
                foreach (var pickableEntity in _pickableFilter.Value)
                {
                    ref var pickableComp = ref _pickablePool.Value.Get(pickableEntity);
                    CheckDistance(requestComp, pickableComp, pickableEntity);
                }
                _requestPool.Value.Del(request);
            }
        }

        private void CheckDistance(CheckDistanceToPickableRequest requestComp, PickableComponent pickableComp, int pickableEntity)
        {
            var distance = requestComp.HookPosition - pickableComp.Pickable.transform.position;
            if (distance.magnitude <= _playerSettings.Value.CatchDistance)
            {
                _catchedPickablePool.Value.Add(pickableEntity);
            }
            DebugExtension.DebugWireSphere(requestComp.HookPosition, Color.yellow, _playerSettings.Value.CatchDistance, 3f);
        }
    }
}