using Mouledoux.Node;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSource : Pipe
{
    public LineRenderer flowViz;
    private Mouledoux.Components.Mediator.Subscription PipeUpdateSub;

    new public void Start()
    {
        base.Start();
        //PipeUpdateSub = new Mouledoux.Components.Mediator.Subscription("PipeModify", SetFlowVizPath).Subscribe();
    }

    void Update()
    {
        SetFlowVizPath(new object[] { });
    }

    private void SetFlowVizPath(object[] args)
    {
        Node[] allPipes = pipeNodeData.GetNeighborhood(999);

        if (allPipes.Length < 1) return;

        Pipe farthestPipe = allPipes[allPipes.Length - 1].GetInformation<Pipe>()[0];
        Stack<Pipe> connectedPipes = Mouledoux.Node.NodeNav.TwinStarT<Pipe>(this, farthestPipe, false);

        flowViz.positionCount = connectedPipes.Count;

        while(connectedPipes.Count > 0)
        {
            flowViz.SetPosition(flowViz.positionCount - connectedPipes.Count, connectedPipes.Pop().transform.position);
        }
    }
}
