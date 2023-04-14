using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public Camera getCamera;
    private RaycastHit hit;
    [SerializeField]
    private Transform destSampleTr = null;

    [Header("--Flag--")]
    [SerializeField]
    private Flag entryFlag = null;//시작플레그만알기
    [SerializeField]
    private Flag goalFlag = null;
    [SerializeField]
    private Flag debug = null;


    private NavMeshAgent agent = null;
    
    private List<Flag> flagList = null;

    public Flag LastvisitFlag = null;
    [SerializeField]
    public Flag goObj = null;


    private bool isMoving = false;
    //스타트 -> 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        //agent.SetDestination(destSampleTr.position);

        //과제
        flagList = FlagManager.PathFinding(entryFlag, goalFlag);


        foreach (Flag flag in flagList)
        {
            Debug.Log("[" + flag + "]");
        }
        SetNextFlag();

        

    }
    
    


    private IEnumerator MovingCoroutine(Flag _nextFlag) //코루틴 병렬작업
    {
        agent.destination = _nextFlag.transform.position;
        isMoving = true;
        yield return null;


        while (agent.remainingDistance > 0f)
            yield return null;

        _nextFlag.SetColor(Flag.EState.Passed);
        isMoving = false;

        SetNextFlag();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        
        
           
            SetNextFlag();
        

        //Debug.Log(agent.remainingDistance);
        //Debug.Log(Input.mousePosition);     //마우스 포지션확인 


        /*//마우스이동 한곳으로 오브젝트이동하고 그위치로 가는 기능
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
        */





        /*
        Debug.Log("시작");

        for (int i = 0; i < FlagManager.flagsB.Length; i++)
        {
            Debug.Log($"{FlagManager.flagsB[i]}");
        }
        Debug.Log("끝");
        Debug.Log(" ");
        Debug.Log(" ");
        */




    }
    public void resetcolor()
    {
        foreach (Flag flag in GameObject.FindObjectsOfType<Flag>()) //Flag 달린 오브젝트 전부
        {
            flag.SetColor(Flag.EState.Normal);
        }
    }
    
    public void SetNextFlag()
    {
        if (isMoving) return;
        if(flagList.Count == 1)
        {
            LastvisitFlag = flagList[0];
            
        }
        if (flagList.Count == 0)return;

        Flag nextFlag = flagList[0];
        //StartCoroutine(MovingCoroutine(nextFlag));
        StartCoroutine("MovingCoroutine", nextFlag);
        flagList.RemoveAt(0);
        //flagList.Remove(nextFlag);
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
    public Flag GetLastFLag()
    {
        return LastvisitFlag;
    }
    public void SetFlag(Flag obj)
    {
        goObj = obj;
    }
    public void SetPathList(List<Flag> _flagList)
    {
        flagList = _flagList;
    }
}
