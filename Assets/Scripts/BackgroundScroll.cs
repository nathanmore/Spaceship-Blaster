using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeedAdjustment = 1.0f;

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        //regular scroll
        offset.y += (Time.deltaTime / 2f) * scrollSpeedAdjustment;
        mat.mainTextureOffset = offset;
    }
}
