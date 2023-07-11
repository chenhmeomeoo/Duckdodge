using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public CinemachineRecomposer camzoom;
    private float zoomIn, zoomOut;
    private float currentZoom;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        zoomIn = 0.25f;
        zoomOut = 1f;
    }
    private void Update()
    {
        camzoom.m_ZoomScale = currentZoom;
    }
    public void ZoomIn()
    {
        DOTween.To(() => currentZoom, x => currentZoom = x, zoomIn, 1f);
    }
    public void ZoomOut()
    {
        DOTween.To(() => currentZoom, x => currentZoom = x, zoomOut, 1f);
    }
}
