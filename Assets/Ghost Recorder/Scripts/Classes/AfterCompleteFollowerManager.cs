using UnityEngine;
using MyRecorder.Settings;

namespace MyRecorder
{
    public class AfterCompleteFollowerManager : MonoBehaviour
    {
        #region variable
        #region public
        public float timeStep;
        #endregion
        #region private
        private DataNodes dataNodes;
        private RecorderSettings recorderSettings;
        private GameObject[] shadows;
        private int shadowsCount;
        private float currentShadowGeneration;
        #endregion
        #endregion
        #region Functions
        private void FixedUpdate()
        {
            updateCycle();
        }
        #endregion
        #region functions
        #region init
        public void InitSettings(DataNodes dataNodes, RecorderSettings settings)
        {
            if (settings == null || dataNodes == null)
            {
                OnDestroyShadowController(); return;
            }
            this.recorderSettings = settings;
            this.dataNodes = new DataNodes(dataNodes);
            this.shadowsCount = settings.shadowsCount;
            if (recorderSettings.Ghost == null || shadowsCount < 1)
            {
                OnDestroyShadowController(); return;
            }
            shadows = new GameObject[shadowsCount];
        }
        #endregion
        #region update
        private void updateCycle()
        {
            //if (recorderSettings == null) return;
            currentShadowGeneration += (timeStep);
            if (currentShadowGeneration > recorderSettings.shadowGenerationRate)
            {
                currentShadowGeneration = 0;
                createFollower();
                if (isReadyForDestroy()) OnDestroyShadowController();
            }

        }
        #endregion
        private bool createFollower()
        {
            if (shadowsCount-- < 1) return false;

            shadows[shadowsCount] = UnityEngine.Object.Instantiate(recorderSettings.Ghost);
            shadows[shadowsCount].transform.SetParent(this.transform);

            ShadowController function = shadows[shadowsCount].AddComponent<ShadowController>();
            function.timeStep = this.timeStep;
            function.dataNodes = this.dataNodes;
            function.initSettings(recorderSettings);
            function.playerState = PlayerState.Play;
            return true;
        }
        #endregion
        #region destructor
        private bool isReadyForDestroy()
        {
            for (int i = 0; i < shadows.Length; i++)
            {
                if (shadows[i] != null) return false;
            }
            return true;
        }
        private void OnDestroyShadowController()
        {

            DestroyImmediate(this.gameObject);
        }
        #endregion
    }
}