using UnityEngine;

public class StaticCamera : MonoBehaviour
{
    public Canvas canvas;

    public void CallingCanvas()
    {
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.transform.position = Vector3.zero;
        canvas.transform.rotation = Quaternion.identity;
        canvas.transform.localScale = Vector3.one;
    }
}
