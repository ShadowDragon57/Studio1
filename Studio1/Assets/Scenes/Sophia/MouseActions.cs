using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseActions : MonoBehaviour
{
    // Start is called before the first frame update
    public Color defaultColour;
    public Color hoverColour;
    public Color ClickColour;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = defaultColour;
    }

    private void OnMouseEnter()
    {
        renderer.material.color = hoverColour;
    }

    private void OnMouseExit()
    {
        renderer.material.color = defaultColour;
    }

    private void OnMouseDown()
    {
        renderer.material.color = ClickColour;
    }

    private void OnMouseUp()
    {
        renderer.material.color = hoverColour;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
