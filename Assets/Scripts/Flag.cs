using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public enum EState
    {
        Normal,Select,Passed
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
                flagMr.material.SetColor("_Color",Color.white);
                break;
            case EState.Select:
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

}
