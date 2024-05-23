using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCamera : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
     
    }

    public void CallingCanvas()
    {
        // Assuming you've already assigned the canvas reference in the Unity Editor
        // If not, you can assign it programmatically using GameObject.FindObjectOfType<Canvas>()

        // Set the render mode to World Space
        canvas.renderMode = RenderMode.WorldSpace;

        // You might need to adjust the canvas's position, rotation, and scale
        // Here, I'll just reset them to identity
        canvas.transform.position = Vector3.zero;
        canvas.transform.rotation = Quaternion.identity;
        canvas.transform.localScale = Vector3.one;
    }
}
