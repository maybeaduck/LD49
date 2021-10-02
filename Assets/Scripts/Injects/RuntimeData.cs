using Zlodey;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Rendering;

namespace Zlodey
{
    [System.Serializable]
    public class RuntimeData
    {
        public bool SoundOn;
        public bool HapticOn;
        public AudioSource AudioSource;
        public GameState GameState;
        public Camera MainCamera;

        [Header("Timers")]
        public float StartLevelTime;
        public float EndLevelTime;

        [Header("Input")]
        public Vector2 Direction;
        public float RotationX;
        public float RotationY;
        public float RotationXRaw;
        public float RotationYRaw;

        [Header("Player")]
        public EcsEntity PlayerEntity;
    }
}