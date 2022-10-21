using NaughtyAttributes;
using UnityEngine;


namespace DemoProject
{
    public class FishingRodHandView : MonoBehaviour
    {
        [field: Foldout("Refrences")] [field: SerializeField] public HookView HookView { get; private set; }

        private void Awake()
        {
            if (!HookView) HookView = FindObjectOfType<HookView>();
        }
    }
}