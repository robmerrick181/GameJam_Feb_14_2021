using UnityEngine;

namespace MyRecorder
{
    [System.Serializable]
    public class DataNode : Shadow<DataNode>
    {
        #region variables
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public int Step { get { return step; } set { step = value; } }
        [HideInInspector] public int step = 0;
        #endregion
        #region constructor
        public DataNode() : this(Vector3.zero, Quaternion.identity) { }
        public DataNode(Transform transform) : this(transform.position, transform.rotation) { }
        public DataNode(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
        public DataNode(DataNode other)
        {
            this.position = other.position;
            this.rotation = other.rotation;
            this.step = other.step;
        }
        #endregion
        #region function
        #region Equals
        public bool EqualsTo(DataNode other)
        {
            if (other == null) return false;
            if (other.position != this.position || other.rotation != this.rotation) return false;
            return true;
        }
        public bool EqualsTo(DataNode other, float[] thresholds)
        {
            if (thresholds == null)
            {
                return EqualsTo(other);
            }
            if (thresholds.Length != 2)
            {
                throw new System.Exception("Not handling yet.");
            }
            return EqualsTo(other, thresholds[0], thresholds[1]);
        }
        public bool EqualsTo(DataNode other, float distanceThreshold, float angleThreshold)
        {
            if (Vector3.Distance(other.position, this.position) > distanceThreshold) return false;
            if (Quaternion.Angle(other.rotation, this.rotation) > angleThreshold) return false;
            return true;
        }
        #endregion
        #endregion
        #region destructor
        ~DataNode()
        {

        }
        #endregion
    }
}