using Leopotam.EcsLite;
using NaughtyAttributes;
using UnityEngine;


namespace DemoProject
{
    public class PickableView : MonoBehaviour
    {
        [field: BoxGroup("Settings")]
        [field: SerializeField]
        public float MovingSpeed { get; private set; } = 2f;

        [field: BoxGroup("Settings")]
        [field: SerializeField]
        public float RotationSpeed { get; private set; } = 2f;
        
        private EcsWorld _world;
        private int _entity;
        
        private void Awake()
        {
            if (gameObject.layer != (int)Layers.Pickable)
            {
                gameObject.layer = (int)Layers.Pickable;
            }
        }

        public void Init(EcsWorld world, int entity)
        {
            _world = world;
            _entity = entity;
        }
    }
}