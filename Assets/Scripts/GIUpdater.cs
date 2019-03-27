using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIUpdater : MonoBehaviour
{
    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        RendererExtensions.UpdateGIMaterials(renderer);
    }
}
