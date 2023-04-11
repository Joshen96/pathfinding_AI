using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform destSampleTr = null;
    
    
    private NavMeshAgent agent = null;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        //agent.SetDestination(destSampleTr.position);
    }
    private void Update()
    {
        //Debug.Log(agent.remainingDistance);
        //Debug.Log(Input.mousePosition);     //마우스 포지션확인
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pickPos = Vector3.zero;

            if(MousePicking(out pickPos))
            {
                destSampleTr.position = pickPos;
            }
            agent.destination = pickPos;
            //agent.SetDestination(destSampleTr.position);
        }
        //agent.SetDestination(destSampleTr.position);

    }

    private bool MousePicking( out Vector3 _pickpos)
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos); //카메라에서 바로쏴줌 ray 값

        RaycastHit hitinfo;//누구인지 받기 

        if(Physics.Raycast(ray, out hitinfo))//레이캐스트같은 충돌은 피직스 클래스
        {
            Debug.Log(hitinfo.transform.name+" : "+ hitinfo.point);
            _pickpos = hitinfo.point;
            return true;
        }
        
        //선택된게 없으면 처리
        _pickpos= Vector3.zero;
        return false;
    }
}
