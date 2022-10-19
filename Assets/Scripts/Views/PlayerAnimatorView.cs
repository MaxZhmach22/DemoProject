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
        private void Awake()
        {
            if (!Animator) Animator = GetComponent<Animator>();
        }

        public void InitEcsWorld(EcsWorld world)
        {
            _world = world;
        }

        public void EndCatchingAnimation()
        {
            Animator.SetBool(_endCatch, false);
        }

        public void SendHook()
        {
            _world.GetPool<HookAnimationEvent>().Add(_world.NewEntity());
            //Debug.Log("Hook");
        }
        
    }
}