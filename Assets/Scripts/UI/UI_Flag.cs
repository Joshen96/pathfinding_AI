using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Flag : MonoBehaviour
{
    private RectTransform rectTr = null;
    public GameObject Flag;

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        
        UpdatePosition(Flag.transform.position);
    }
    public void UpdatePosition(Vector3 _pos) //위치 받아와서  갱신
    {

        Vector3 w2c = Camera.main.WorldToScreenPoint(Flag.transform.position); //월드 포지션을 스크린포지션으로 
        rectTr.position = w2c + new Vector3(0f, -30f, 0f);
    }
    public GameObject GetFlag()
    {
        return Flag;
    }
}
