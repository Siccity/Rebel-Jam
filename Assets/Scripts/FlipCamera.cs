using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FlipCamera : MonoBehaviour
{
    private Camera camera;
    public bool flipHorizontal;
    public bool flipVertical;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void OnPreCull()
    {
        camera.ResetWorldToCameraMatrix();
        camera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, flipVertical ? -1 : 1, 1);
        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(scale);
    }

    void OnPreRender()
    {
        GL.invertCulling = flipHorizontal || flipVertical;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
