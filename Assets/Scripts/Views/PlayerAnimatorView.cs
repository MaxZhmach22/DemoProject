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

        private void Awake()
        {
            if (!Animator) Animator = GetComponent<Animator>();
        }

        public void EndCatchingAnimation()
        {
            Animator.SetBool(_endCatch, false);
        }
        
    }
}