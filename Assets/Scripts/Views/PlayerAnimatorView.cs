using Leopotam.EcsLite;
using NaughtyAttributes;
using UnityEngine;


namespace DemoProject
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorView : MonoBehaviour
    {
        [field: Foldout("References")] [field: SerializeField] public Animator Animator { get; private set; }
        private readonly int _endCatch = Animator.StringToHash("IsCatching");

        private EcsWorld _world;
        private int _playerEntity;
        private void Awake()
        {
            if (!Animator) Animator = GetComponent<Animator>();
        }

        public void InitEcsWorld(EcsWorld world, int playerEntity)
        {
            _world = world;
            _playerEntity = playerEntity;
        }

        public void EndCatchingAnimation()
        {
            Animator.SetBool(_endCatch, false);
            if (_world.GetPool<IsCatchingMarker>().Has(_playerEntity))
            {
                _world.GetPool<IsCatchingMarker>().Del(_playerEntity);
            }
        }

        public void ResetRotation()
        {
            transform.localRotation = Quaternion.identity;
        }

        public void SendHook()
        {
            if (!_world.GetPool<IsCatchingMarker>().Has(_playerEntity))
            {
                _world.GetPool<IsCatchingMarker>().Add(_playerEntity);
            }
            
            _world.GetPool<HookAnimationEvent>().Add(_world.NewEntity());
            //Debug.Log("Hook");
        }
    }
}