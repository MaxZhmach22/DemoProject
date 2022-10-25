using System;
using UnityEngine;

namespace DemoProject
{
    public class OutOfScreenTest : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            IsOutOfScreen(transform);
        }


        private void IsOutOfScreen(Transform transform)
        {
            Debug.Log($"{_camera.WorldToScreenPoint(transform.position)} {transform.position}");
            _camera.ScreenPointToRay(Vector3.zero);
        }
    }
}