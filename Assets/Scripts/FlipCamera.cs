using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class FlipCamera : MonoBehaviour
{
    private Camera camera;
    public bool FlipHorizontal;
    public bool FlipVertical;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void OnPreCull()
    {
        camera.ResetWorldToCameraMatrix();
        camera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(FlipHorizontal ? -1 : 1, FlipVertical ? -1 : 1, 1);
        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(scale);
    }

    void OnPreRender()
    {
        GL.invertCulling = FlipHorizontal || FlipVertical;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
