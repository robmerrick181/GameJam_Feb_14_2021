using MyRecorder.Settings;
using UnityEngine;

namespace MyRecorder
{
    public class RealTimeShadowController : ShadowController
    {
        #region variable
        #region public
        public GameObject recorder;
        #endregion
        #region private
        #endregion
        #endregion
        #region functions
        #region data nodes


        #endregion
        protected override void manageDataNodes()
        {
            dataNodes = recorder.GetComponent<Recorder>().dataNodes;
        }
        protected override void onTrackedPath()
        {
            if (recorderSettings.deleteTrackedPath)
            {
                recorder.GetComponent<Recorder>().dataNodes.Nodes_remove(index--);
            }
        }
        public override void initSettings(RecorderSettings settings)
        {
            if (settings == null) return;
            recorderSettings = settings;
        }
        #endregion
        #region destructor
        protected override void OnDestroyShadowController()
        {
            recorder.GetComponent<Recorder>().RemoveRealTimeFollower();
            DestroyImmediate(this.gameObject);
        }
        #endregion
    }
}
