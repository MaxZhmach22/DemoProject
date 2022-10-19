using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace DemoProject
{
    public class UnitTransformInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<UnitTransformComponent> _pool = default;
        private readonly EcsFilterInject<Inc<UnitTransformComponent>> _filter = default;

        public void Init(IEcsSystems systems)
        {
            
        }
    }
}