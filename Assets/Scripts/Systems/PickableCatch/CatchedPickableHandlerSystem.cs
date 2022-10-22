using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class CatchedPickableHandlerSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<PickableCatchRequest> _requestPool = default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsFilterInject<Inc<PickableComponent, PickableCatchRequest>> _catchedFilter = default;
        private readonly Transform _fishBuckedParent = new GameObject("Bucket").transform;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _catchedFilter.Value)
            {
                ref var pickableComp = ref _pickablePool.Value.Get(entity);

                Handle(pickableComp);
                
                _requestPool.Value.Del(entity);
            }
        }

        private void Handle(PickableComponent pickableComp)
        {
            Debug.Log("PickedUp");
            pickableComp.Pickable.gameObject.SetActive(false);
            pickableComp.Pickable.transform.position = _fishBuckedParent.position;
            pickableComp.Pickable.transform.SetParent(_fishBuckedParent);
        }
    }
}