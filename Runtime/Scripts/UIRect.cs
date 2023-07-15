using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[AddComponentMenu("UI/Rect")]
public class UIRect : Graphic
{

    [SerializeField] private Vector4 _radii;
    [SerializeField] private bool _border;
    [SerializeField] private float _borderWidth;


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        var r = GetPixelAdjustedRect();
        var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);

        //SDF data:
        // x: width
        // y: height
        // z: borderWidth
        // w: unused
        Vector4 sdfData = new()
        {
            x = r.width,
            y = r.height,
            z = _border ? _borderWidth : Mathf.Max(r.width, r.height)
        };

        vh.Clear();
        UIVertex vert = new()
        {
            position = new Vector3(v.x, v.y),
            color = color,
            uv0 = new Vector2(0f,0f),
            uv1 = sdfData,
            uv2 = _radii
        };
        vh.AddVert(vert);
        vert = new()
        {
            position = new Vector3(v.x, v.w),
            color = color,
            uv0 = new Vector2(0f,1f),
            uv1 = sdfData,
            uv2 = _radii
        };
        vh.AddVert(vert);
        vert = new()
        {
            position = new Vector3(v.z, v.w),
            color = color,
            uv0 = new Vector2(1f,1f),
            uv1 = sdfData,
            uv2 = _radii
        };
        vh.AddVert(vert);
        vert = new()
        {
            position = new Vector3(v.z, v.y),
            color = color,
            uv0 = new Vector2(1f,0f),
            uv1 = sdfData,
            uv2 = _radii
        };
        vh.AddVert(vert);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
}
