using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Zlodey
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Props")]
        public Levels Levels;
        public ObjectPoolController ObjectPooler;

        [Header("Prefabs")]
        public AudioSource AudioSourcePrefab;
        public UI UIPrefab;

        [Header("Input")]
        public float MouseSensitivity = 1f;

        [Header("Timers")]
        public float StartTimeToDestruction = 240f;
        public float StableToPhase2 = .5f;
        public float StableToPhase3 = .25f;

        public float distanceToInteract;
    }
}