using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BlackScreen : MonoBehaviour
{
    public Shader shader;
    Material _mat;

    [Range(0,1)]
    public float blackScreenIntensity;

    private void Awake()
    {
        _mat = new Material(shader);
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _mat);
    }

    public void Update()
    {
        _mat.SetFloat("_intensity", blackScreenIntensity);
    }
}
