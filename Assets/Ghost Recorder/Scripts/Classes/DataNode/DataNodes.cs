using UnityEngine;
using System.Collections.Generic;

namespace MyRecorder
{
    [System.Serializable]
    public class DataNodes
    {
        #region variable
        #region public
        [SerializeField]
        //[HideInInspector]
        public List<DataNode> Nodes;
        [HideInInspector] public float timeStep;
        [HideInInspector] public float totalTimeStep;
        [Min(0)]
        public float distanceThreshold;
        [Range(0, 360)]
        public float angleThreshold;
        #endregion
        #endregion
        #region constructor
        public DataNodes() : this(.1f, .2f, 2) { }
        public DataNodes(float timeStep, float distanceThreshold, float angleThreshold)
        {
            this.timeStep = timeStep;
            this.distanceThreshold = distanceThreshold;
            this.angleThreshold = angleThreshold;
            Nodes_reset();
        }
        public DataNodes(DataNodes other)
        {
            if (other == null) { new DataNodes(); return; }
            Nodes_reset();
            this.timeStep = other.timeStep;
            this.distanceThreshold = other.distanceThreshold;
            this.angleThreshold = other.angleThreshold;
            this.totalTimeStep = other.totalTimeStep;
            Nodes_copy(other.Nodes);
        }
        #endregion
        #region functions
        #region totalTimeStep
        public void totalTimeStep_reset()
        {
            totalTimeStep = 0;
        }
        public void totalTimeStep_remove(int index)
        {
            DataNode node = Nodes[index];
            totalTimeStep -= (node.Step + 1);
        }
        public void totalTimeStep_increase()
        {
            totalTimeStep_increase(1);
        }
        public void totalTimeStep_increase(int val)
        {
            totalTimeStep += val;
        }
        #endregion
        #region Nodes
        public DataNode getNode(int index)
        {
            if (Nodes == null || Nodes.Count <= index) return null;
            return Nodes[index];
        }
        public bool hasNode()
        {
            return Nodes != null && Nodes.Count > 0;
        }
        public int Nodes_count()
        {
            return Nodes == null ? -1 : Nodes.Count;
        }
        public void Nodes_reset()
        {
            totalTimeStep_reset();
            Nodes = new List<DataNode>();
        }
        public void Nodes_copy(List<DataNode> other)
        {
            if (other == null) return;
            for (int i = 0; i < other.Count; i++)
            {
                //totalTimeStep_increase(other[i].Step);
                Nodes.Add(new DataNode(other[i]));
            }
        }
        public void Nodes_remove(int index)
        {
            if (Nodes == null || Nodes.Count <= index) return;
            totalTimeStep_remove(index);
            Nodes.RemoveAt(index);
        }
        #region add
        public bool Nodes_add(Transform transform)
        {
            if (transform == null) return false;
            return Nodes_add(new DataNode(transform));
        }
        public bool Nodes_add(Vector3 position)
        {
            return Nodes_add(position, Quaternion.identity);
        }
        public bool Nodes_add(Vector3 position, Quaternion rotation)
        {
            if (position == null || rotation == null) return false;
            return Nodes_add(new DataNode(position, rotation));
        }
        public bool Nodes_add(DataNode node)
        {
            if (node == null) return false;
            if (Nodes.Count > 0)
            {
                DataNode dn = Nodes[Nodes.Count - 1];
                if (dn.EqualsTo(node, new float[] { distanceThreshold, angleThreshold }))
                {
                    totalTimeStep_increase();
                    dn.Step++;
                    return true;
                }
            }
            totalTimeStep_increase(node.Step + 1);
            Nodes.Add(node);
            return true;
        }
        #endregion
        #endregion
        #endregion
        #region destructor
        ~DataNodes() { }
        #endregion
    }
}