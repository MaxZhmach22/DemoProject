using NaughtyAttributes;
using UnityEngine;


namespace DemoProject
{
    public class PlayerSettings : MonoBehaviour
    {
        [field: Space(20)]
        [field: BoxGroup("Move Settings:")] [field: SerializeField] public float WalkSpeed { get; private set; }
        
        [field: Space(20)]
        [field: BoxGroup("Hook Settings:")] [field: SerializeField] public LayerMask GroundMask { get; private set; }
        [field: BoxGroup("Hook Settings:")] [field: SerializeField] public LayerMask DoubleClickRaycast { get; private set; }
        [field: BoxGroup("Hook Settings:")] [field: SerializeField] public float TimeHookMove { get; private set; }
    }
}