using NaughtyAttributes;
using UnityEngine;


namespace DemoProject
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorView : MonoBehaviour
    {
        [field: Foldout("References")] [field: SerializeField] public Animator Animator { get; private set; }

        private void Awake()
        {
            if (!Animator) Animator = GetComponent<Animator>();
        }
    }
}