using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, Mouledoux.Node.ITraversable
{
    public Mouledoux.Node.Node pipeNodeData;
    public float pressure;


    private void Initialize()
    {
        pipeNodeData.RemoveAllInformation(true);
        pipeNodeData.AddInformation(this);
    }

    public void Flow()
    {
        foreach(Mouledoux.Node.Node neighbor in pipeNodeData.GetNeighborhood())
        {
            if(neighbor.GetInformation<Pipe>()[0] is Pipe connectedPipe)
            {
                
            }
        }
    }
}
