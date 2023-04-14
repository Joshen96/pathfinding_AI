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
    public void UpdatePosition(Vector3 _pos) //��ġ �޾ƿͼ�  ����
    {

        Vector3 w2c = Camera.main.WorldToScreenPoint(Flag.transform.position); //���� �������� ��ũ������������ 
        rectTr.position = w2c + new Vector3(0f, -30f, 0f);
    }
    public GameObject GetFlag()
    {
        return Flag;
    }
}
