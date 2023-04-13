using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class DebugFlag : MonoBehaviour
{
    [SerializeField]
    private bool isShow = true;
    [SerializeField]
    private Color color = Color.white;

    private void Update()
    {
        if (!isShow) return;

        Flag[] nextFlags =
            GetComponent<Flag>().GetNextFlags();

        foreach (Flag flag in nextFlags)
        {
            Debug.DrawLine(
                transform.position,
                flag.transform.position,
                color);
            float dist = Vector3.Distance(transform.position , flag.transform.position);

            // Debug.Log("거리보기"+gameObject.name+"~"+flag.name +"=="+ dist);


            



        }
        


    }
    
}
