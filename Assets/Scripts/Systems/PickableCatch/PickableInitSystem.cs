using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DemoProject
{
    public class PickableInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<PlayerSettings> _playerSettings = default;
        private readonly EcsPoolInject<PickableComponent> _pickablePool = default;
        private readonly EcsPoolInject<InPoolMarker> _markerPool = default;
        private readonly Transform _poolTransform = new GameObject("Pickable Pool").transform;
        

        public void Init(IEcsSystems systems)
        {
            InitPickablePrefabs();
        }

        private void InitPickablePrefabs()
        {
            foreach (var prefab in _playerSettings.Value.PrefabsList)
            {
                for (int i = 0; i < _playerSettings.Value.CountToInstantiate; i++)
                {
                    var go = Object.Instantiate(prefab, _poolTransform);
                    go.gameObject.SetActive(false);
                    var entity = _world.Value.NewEntity();
                    ref var pickableComp = ref _pickablePool.Value.Add(entity);
                    _markerPool.Value.Add(entity);
                    pickableComp.Pickable = go;
                }
            }
        }
    }
}