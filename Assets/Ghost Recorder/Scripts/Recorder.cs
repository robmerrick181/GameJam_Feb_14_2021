using UnityEngine;
using MyRecorder.Settings;

namespace MyRecorder
{
    public class Recorder : MonoBehaviour
    {
        #region variable
        #region public
        public RecorderState recorderState = RecorderState.Idle;
        public RecorderSettings recorderSettings;
        [HideInInspector] public DataNodes dataNodes;
        [HideInInspector] public DataNodes bestBenefitOfDataNodes = null;
        [HideInInspector] public float timeStep;
        #endregion
        #region private
        private RecorderState lastRecordState = RecorderState.Idle;
        private float sampling;
        private GameObject garbage;
        #region realtime
        private float currentShadowGeneration;
        private GameObject realTimeFollower;
        #endregion
        #endregion
        #endregion
        #region Functions
        private void OnValidate()
        {
            if (this.transform == null)
            {
                Debug.Log("Transform not found.");
                Application.Quit();
            }
            if (recorderSettings == null)
            {
                Debug.Log("Recorder settings not found.");
                Application.Quit();
            }
        }
        void Start()
        {
            timeStep = Time.fixedDeltaTime;
            autoLoad();
        }
        void FixedUpdate()
        {
            updateRecording();
            if (recorderSettings.recorderSettingsType == RecorderSettingsType.RealTime)
            {
                realTimeGenerateUpdate();
            }
        }
        #region Trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other == this) return;
            string otherName = other.name;
            if (string.IsNullOrEmpty(otherName)) return;
            otherName = otherName.ToLower();
            if (otherName.IndexOf("ghost_trigger") > -1)
            {
                if (otherName.IndexOf("start") > -1)
                {
                    recorderState = RecorderState.Recording;
                }
                else if (otherName.IndexOf("finish") > -1)
                {
                    recorderState = RecorderState.Idle;
                }
                else
                {
                    if (recorderState == RecorderState.Idle)
                    {
                        recorderState = RecorderState.Recording;
                    }
                    else
                    {
                        recorderState = RecorderState.Idle;
                    }
                }
            }
        }
        #endregion
        #endregion
        #region functions
        #region init
        private void initRecording()
        {
            sampling = 0;
            dataNodes = new DataNodes(timeStep, recorderSettings.distanceThreshold, recorderSettings.angleThreshold);
        }
        #endregion
        #region Clean and clear
        public bool Clear()
        {
            if (lastRecordState != RecorderState.Idle) return false;
            initRecording();
            bestBenefitOfDataNodes = null;
            return true;
        }
        #endregion
        #region recording
        public void StartRecording()
        {
            if (recorderSettings.recorderSettingsType == RecorderSettingsType.RealTime)
            {
                if (realTimeFollower != null)
                {
                    lastRecordState = recorderState = RecorderState.Recording;
                    return;
                }
            }
            else if (recorderSettings.recorderSettingsType == RecorderSettingsType.RealTime)
            {

            }
            initRecording();
            lastRecordState = recorderState = RecorderState.Recording;

        }
        public void StopRecording()
        {
            lastRecordState = recorderState = RecorderState.Idle;
            //sampling = 0;
            if (recorderSettings.saveBestBenefit)
            {
                if (bestBenefitOfDataNodes == null || bestBenefitOfDataNodes.Nodes_count() < 1)
                {
                    bestBenefitOfDataNodes = new DataNodes(dataNodes);
                    autoSave();
                }
                else if (bestBenefitOfDataNodes.totalTimeStep > dataNodes.totalTimeStep)
                {
                    bestBenefitOfDataNodes = new DataNodes(dataNodes);
                    autoSave();
                }
            }
        }
        private void updateRecording()
        {
            if (recorderState != lastRecordState)
            {
                if (lastRecordState == RecorderState.Idle)
                {
                    StartRecording();
                }
                else
                {
                    StopRecording();
                    createAfterCompleteFollower();
                }
            }
            if (lastRecordState == RecorderState.Recording)
            {
                sampling += (timeStep);
                if (sampling < recorderSettings.sampling) { }
                else
                {
                    sampling = 0;
                    dataNodes.Nodes_add(this.transform);
                }
            }
        }
        #endregion
        #region realtime
        public void RemoveRealTimeFollower()
        {
            realTimeFollower = null;
        }
        private void realTimeGenerateUpdate()
        {
            if (lastRecordState != RecorderState.Recording || realTimeFollower != null) return;
            currentShadowGeneration += (timeStep);
            if (currentShadowGeneration > recorderSettings.shadowGenerationRate)
            {
                currentShadowGeneration = 0;
                createRealTimeFollower();
            }
        }
        private bool createRealTimeFollower()
        {
            if (recorderSettings.Ghost == null || realTimeFollower != null) return false;
            realTimeFollower = UnityEngine.Object.Instantiate(recorderSettings.Ghost);

            RealTimeShadowController function = realTimeFollower.AddComponent<RealTimeShadowController>();
            function.recorder = this.gameObject;
            function.timeStep = this.timeStep;
            function.initSettings(recorderSettings);
            function.playerState = PlayerState.Play;
            return true;
        }
        #endregion
        #region after complete
        private void createAfterCompleteFollower()
        {
            createAfterCompleteFollower(ref dataNodes);
        }
        private void createAfterCompleteFollower(ref DataNodes dns)
        {
            if (dns == null || dns.Nodes_count() < 1) return;
            if (garbage == null)
            {
                garbage = new GameObject("garbage");
            }
            if (recorderSettings.recorderSettingsType != RecorderSettingsType.AfterComplete) return;
            GameObject afterCompleteFollowerManager = new GameObject("After Complete Follower Manager");
            afterCompleteFollowerManager.transform.SetParent(garbage.transform);
            var function = afterCompleteFollowerManager.AddComponent<AfterCompleteFollowerManager>();
            function.timeStep = timeStep;
            function.InitSettings(dns, recorderSettings);
        }
        #endregion
        #region save and load
        private void autoSave()
        {
            if (!recorderSettings.autoLoadAndStoreBestBenefit) return;
            Save();
        }
        public void Save()
        {
            PlayerPrefs.SetString(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString(), JsonUtility.ToJson(bestBenefitOfDataNodes));
        }
        private void autoLoad()
        {
            if (!recorderSettings.autoLoadAndStoreBestBenefit) return;
            Load();
        }
        public void Load()
        {
            string json = PlayerPrefs.GetString(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString());
            if (string.IsNullOrEmpty(json)) return;
            bestBenefitOfDataNodes = JsonUtility.FromJson<DataNodes>(json);
        }
        public void Play()
        {
            createAfterCompleteFollower(ref bestBenefitOfDataNodes);
        }
        #endregion
        #endregion
    }
}