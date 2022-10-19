using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace DemoProject
{
    public class GroundCheckSystem :  IEcsRunSystem
    {
        private readonly EcsCustomInject<GroundChecker> _groundCheker = default;
        private readonly EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsPoolInject<NoGroundMarker> _pool = default;
        private readonly EcsFilterInject<Inc<PlayerTransformComponent>> _filter = default;
        private RaycastHit[] _hits = new RaycastHit[3];

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                CheckGround(entity);
            }
        }

        private void CheckGround(int entity)
        {
            int hits = Physics.RaycastNonAlloc(_groundCheker.Value.transform.position, Vector3.down, _hits, 10f,
                _playerSettings.Value.GroundMask);

            if (hits == 0)
            {
                if (!_pool.Value.Has(entity))
                {
                    _pool.Value.Add(entity);
                    //Debug.Log("No Ground");
                }
            }
            else
            {
                if (_pool.Value.Has(entity))
                {
                    _pool.Value.Del(entity);
                }
            }
        }
    }
}