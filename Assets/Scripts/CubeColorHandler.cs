using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeColorHandler : MonoBehaviour
{
    private Color original;
    private MeshRenderer meshRenderer;

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        meshRenderer.material.color = Color.blue;
    }

    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        meshRenderer.material.color = original;
    }

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        original = meshRenderer.material.color;
    }
}
