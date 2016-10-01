using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour
{
    public string layername;
    public int layerOrder;
    void Start()
    {
        // Set the sorting layer of the particle system.
        this.GetComponent<Renderer>().sortingLayerName = layername;
        this.GetComponent<Renderer>().sortingOrder = layerOrder;
    }
}
