using UnityEngine;

namespace DemoProject
{
    public class PickableView : MonoBehaviour
    {
        private void Awake()
        {
            if (gameObject.layer != (int)Layers.Pickable)
            {
                gameObject.layer = (int)Layers.Pickable;
            }
        }
    }
}