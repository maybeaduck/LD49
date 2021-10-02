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

        [Header("Timers")]
        public float StartLevelTime;
        public float EndLevelTime;
    }
}