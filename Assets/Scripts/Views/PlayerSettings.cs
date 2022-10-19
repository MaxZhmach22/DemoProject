﻿using NaughtyAttributes;
using UnityEngine;


namespace DemoProject
{
    public class PlayerSettings : MonoBehaviour
    {
        [field: BoxGroup("Settings")] [field: SerializeField] public float WalkSpeed { get; private set; }
        [field: BoxGroup("Settings")] [field: SerializeField] public LayerMask GroundMask { get; private set; }
    }
}