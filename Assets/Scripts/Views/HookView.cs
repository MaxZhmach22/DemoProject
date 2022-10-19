using NaughtyAttributes;
using Obi;
using UnityEngine;


namespace DemoProject
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody),
        typeof(ObiRigidbody), 
        typeof(ObiCollider))]
    public class HookView : MonoBehaviour
    {
        [field: Foldout("Refernces")] [field: SerializeField] public ObiRigidbody ObiRigidbody { get; private set; }
        [field: Foldout("Refernces")] [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: Foldout("Refernces")] [field: SerializeField] public Collider Collider { get; private set; }
        [field: Foldout("Refernces")] [field: SerializeField] public MeshRenderer HookRender { get; private set; }

        private void Awake()
        {
            if (!ObiRigidbody) GetComponent<ObiRigidbody>();
            if (!Rigidbody) GetComponent<Rigidbody>();
            if (!Collider) GetComponent<Collider>();
            if (!HookRender) GetComponentInChildren<MeshRenderer>();
        }

        [Button("Get References")]
        private void Reference()
        {
            ObiRigidbody = GetComponent<ObiRigidbody>();
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            HookRender = GetComponentInChildren<MeshRenderer>();
        }
    }
}