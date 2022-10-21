using System.Collections.Generic;
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
        private readonly EcsFilterInject<Inc<JoysticInputComponent>, Exc<IsCatchingMarker>> _filter = default;
        private bool _isSwitched;
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
            SwitchRods( Mathf.Abs(input.Horizontal) + Mathf.Abs(input.Vertical) > 0.1);
        }

        private void SwitchRods(bool status)
        {
            if(_isSwitched == status) return;

            _isSwitched = status;
            var renderers = new List<Renderer>();
            renderers.AddRange(_handRod.Value.GetComponentsInChildren<Renderer>());
            renderers.AddRange(_handRod.Value.HookView.GetComponentsInChildren<Renderer>());
            renderers.ForEach(r => r.enabled = !status);
            if (!status)
            {
                _handRod.Value.HookView.ObiRope.ResetParticles();
                _handRod.Value.HookView.Rigidbody.velocity = Vector3.zero;
            }
                
            _spineRod.Value.gameObject.SetActive(status);
        }
    }
}