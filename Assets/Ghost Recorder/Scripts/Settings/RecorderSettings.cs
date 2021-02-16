using UnityEngine;

namespace MyRecorder.Settings
{
    [CreateAssetMenu()]
    public class RecorderSettings : ScriptableObject
    {

        public GameObject Ghost;
        [Header("Sampling")]
        [Range(.01f, 1)]
        public float sampling = .01f;
        [Min(0)]
        public float distanceThreshold = .1f;
        [Range(0, 360)]
        public float angleThreshold = 2;
        [Header("Generation")]
        public MovementMode movementMode = MovementMode.Transform;
        public RecorderSettingsType recorderSettingsType = RecorderSettingsType.AfterComplete;
        [HideInInspector] public bool deleteTrackedPath = false;
        [HideInInspector] public bool saveBestBenefit = false;
        [HideInInspector] public int shadowsCount = 0;
        [HideInInspector] public float shadowGenerationRate = 5;
        [HideInInspector] public bool autoLoadAndStoreBestBenefit = false;
    }
}