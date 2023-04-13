using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public enum EState
    {
        Normal, Selected, Passed
    }
    [SerializeField]
    private MeshRenderer flagMr = null;

    //색 변경 다음 플래그 
    [SerializeField]
    private List<Flag> nextFlagList = new List<Flag>();

    public void SetColor(EState _state)
    {
        switch (_state)
        {
            case EState.Normal:
                flagMr.material.SetColor("_Color", Color.white);
                break;
            case EState.Selected:
                flagMr.material.SetColor("_Color", Color.red);
                break;
            case EState.Passed:
                flagMr.material.SetColor("_Color", Color.yellow);
                break;
        }
    }
    public Flag[] GetNextFlags()
    {
        return nextFlagList.ToArray();
    }
    public string GetName()
    {
        return gameObject.name;
    }

    public float getDis(Flag _nextFlag)
    {


        float Dis = Vector3.Distance(gameObject.transform.position,_nextFlag.gameObject.transform.position);

        return Dis;

    }
}

