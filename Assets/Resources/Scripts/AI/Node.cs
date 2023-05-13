using System.Collections.Generic;

namespace LaninCode
{
    public class Node
    {
        public enum NodeState
        {
            Running,
            Success,
            Failure
        }
        
        public NodeState SelectedNodeState { get; private set; }
        public Node ParentNode { get; set; }
        public List<Node> ChildNodes { get; }=new List<Node>();
        
        
    }
}