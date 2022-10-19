using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class FishingRodSwitcherSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<FishingRodSpineView> _spineRod;
        private readonly EcsCustomInject<FishingRodHandView> _handRod;
        private readonly EcsPoolInject<JoysticInputComponent> _pool = default;
        private readonly EcsFilterInject<Inc<JoysticInputComponent>> _filter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                SwitchFishingRode(entity);
            }
        }

        private void SwitchFishingRode(int entity)
        {
            ref var input = ref _pool.Value.Get(entity);
            if (Mathf.Abs(input.Horizontal) + Mathf.Abs(input.Vertical) > 0.1)
            {
                _spineRod.Value.gameObject.SetActive(true);
                _handRod.Value.gameObject.SetActive(false);
            }
            else
            {
                _handRod.Value.gameObject.SetActive(true);
                _spineRod.Value.gameObject.SetActive(false);
            }
        }
    }
}