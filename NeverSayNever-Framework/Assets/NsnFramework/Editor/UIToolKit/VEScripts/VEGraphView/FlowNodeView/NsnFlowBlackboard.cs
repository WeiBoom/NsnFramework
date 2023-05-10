using UnityEditor.Experimental.GraphView;

namespace Nsn.EditorToolKit
{
    public class NsnFlowBlackboard : Blackboard
    {
        private NsnBaseGraphView m_GraphView;
        
        public NsnFlowBlackboard(NsnBaseGraphView graphView) :base(graphView)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Add(new BlackboardSection { title = "Exposed Variables"});
        }
    }
}