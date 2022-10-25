using System;
using NaughtyAttributes;
using UnityEngine;

namespace DemoProject
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraBoundView : MonoBehaviour
    {
        [field: Foldout("Bounds")] [field: SerializeField] public float MinZValue { get; private set; }
        [field: Foldout("Bounds")] [field: SerializeField] public float MaxZValue { get; private set; }
        [field: Foldout("References")] [field: SerializeField] public Camera Camera { get; private set; }
        [field: Foldout("References")] [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
        [field: Foldout("References")] [field: SerializeField] public Transform IceLand { get; private set; }


        [Button("Find Screen Bounds")]
        private void GameScreenBounds()
        {
            var position = Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, (IceLand.transform.position - Camera.transform.position).magnitude));
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = position;
            
        }
        
       
        

        private void OnDrawGizmos()
        {
           
        }
    }
}