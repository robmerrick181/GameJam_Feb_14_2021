using UnityEngine;
using MyRecorder.Settings;

namespace MyRecorder
{
    public class ShadowController : MonoBehaviour
    {
        #region variable
        #region public
        public PlayerState playerState = PlayerState.Stop;
        public RecorderSettings recorderSettings;
        public float timeStep;
        public DataNodes dataNodes;
        #endregion
        #region private
        protected RecorderSettingsType recorderSettingsType;

        protected Rigidbody rb;
        private float sampling;
        protected int index;
        private int step;
        private DataNode target;
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
        }
        void Start()
        {
            if (recorderSettings == null)
            {
                Debug.Log("Recorder settings not found.");
                Application.Quit();
            }
            if (timeStep <= 0)
            {
                Debug.Log("Time step not set.");
                Application.Quit();
            }
            if (recorderSettings.movementMode == MovementMode.RigidBody)
            {
                if ((rb = this.gameObject.GetComponent<Rigidbody>()) == null)
                {
                    Debug.Log("Rigidbody not found.");
                    Application.Quit();
                }
            }
        }

        void FixedUpdate()
        {
            updatePlaying();
        }
        private void LateUpdate()
        {
            movement();
        }
        #endregion
        #region functions
        public virtual void initSettings(RecorderSettings settings)
        {
            recorderSettings = settings;
        }
        #region data nodes
        public void InitDataNodes(DataNodes input)
        {
            if (input == null) return;
            dataNodes = new DataNodes(input);
            timeStep = dataNodes.timeStep;
        }
        #endregion
        private void updatePlaying()
        {
            if (playerState != PlayerState.Play) return;

            sampling += timeStep;
            if (sampling < (recorderSettings.sampling)) return;
            //dataNodes = recorder.GetComponent<Recorder>().getManager();
            sampling = 0;
            manageDataNodes();
            setTarget();
            if (target != null && target.Step == step)
            {
                step = 0;
                onTrackedPath();
                index++;
            }
            else step++;
        }
        protected virtual void manageDataNodes()
        {
            //dataNodes = recorder.GetComponent<Recorder>().dataNodes;
        }
        protected virtual void onTrackedPath() { }
        protected virtual void setTarget()
        {
            if ((target = dataNodes.getNode(index)) == null)
            {
                index = 0;
                playerState = PlayerState.Stop;
                OnDestroyShadowController();
            }
        }
        #endregion
        #region destructor
        protected virtual void OnDestroyShadowController()
        {
            DestroyImmediate(this.gameObject);
        }
        #endregion
        #region Movement
        private void movement()
        {
            if (target == null) return;
            this.transform.rotation = target.rotation;
            if (recorderSettings.movementMode == MovementMode.Transform)
            {
                this.transform.position = target.position;
            }
            else if (recorderSettings.movementMode == MovementMode.RigidBody)
            {
                rb.MovePosition(target.position);
            }
        }
        #endregion
    }
}