using Mouledoux.Node;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, Mouledoux.Node.ITraversable
{
    public Mouledoux.Node.Node pipeNodeData;
    public float pressure;

    public ITraversable origin { get; set; }
    public float fVal => gVal + hVal;
    public float gVal { get; set; }
    public float hVal { get; set; }
    public bool isOccupied { get; set; }
    public bool isTraversable { get; set; }

    public float[] pathingValues { get; set; }
    private float _pressure => pressure;

    public float[] coordinates { get; set; }
    public float _cX => transform.position.x;
    public float _cY => transform.position.y;
    public float _cZ => transform.position.z;

    public void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        pipeNodeData = new Node();
        pipeNodeData.RemoveAllInformation(true);
        pipeNodeData.AddInformation(this);

        pathingValues = new float[] { _pressure };
        coordinates = new float[] { _cX, _cY, _cZ };

        isOccupied = false;
        isTraversable = true;
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

    private void OnTriggerEnter(Collider other)
    {
        Pipe connectedPipe = other.GetComponent<Pipe>();
        if(connectedPipe != null)
        {
            pipeNodeData.AddNeighbor(connectedPipe.pipeNodeData);
            Mouledoux.Components.Mediator.NotifySubscribers("PipeModify");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Pipe connectedPipe = other.GetComponent<Pipe>();
        if (connectedPipe != null)
        {
            pipeNodeData.RemoveNeighbor(connectedPipe.pipeNodeData);
            Mouledoux.Components.Mediator.NotifySubscribers("PipeModify");
        }
    }

    public ITraversable[] GetConnectedTraversables()
    {
        List<ITraversable> tn = new List<ITraversable>();
        foreach (Node nd in pipeNodeData.GetNeighbors())
        {
            tn.Add(nd.GetInformation<ITraversable>()[0]);
        }

        return tn.ToArray();
    }


    public int CompareTo(ITraversable other)
    {
        float fDif = fVal - other.fVal;
        float pDif = 0;
        float returnDif = 0;

        for(int i = 0; i < pathingValues.Length; i++)
        {
            pDif += pathingValues[i];
        }
        for (int i = 0; i < other.pathingValues.Length; i++)
        {
            pDif -= other.pathingValues[i];
        }

        returnDif = fDif + pDif;

        return returnDif > 0 ? 1 : returnDif < 0 ? -1 : 0;
    }
}
