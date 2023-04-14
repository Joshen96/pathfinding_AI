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
    private Flag entryFlag = null;//�����÷��׸��˱�
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
    //��ŸƮ -> 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        //agent.SetDestination(destSampleTr.position);

        //����
        flagList = FlagManager.PathFinding(entryFlag, goalFlag);


        foreach (Flag flag in flagList)
        {
            Debug.Log("[" + flag + "]");
        }
        SetNextFlag();

        

    }
    
    


    private IEnumerator MovingCoroutine(Flag _nextFlag) //�ڷ�ƾ �����۾�
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
        //Debug.Log(Input.mousePosition);     //���콺 ������Ȯ�� 


        /*//���콺�̵� �Ѱ����� ������Ʈ�̵��ϰ� ����ġ�� ���� ���
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
        Debug.Log("����");

        for (int i = 0; i < FlagManager.flagsB.Length; i++)
        {
            Debug.Log($"{FlagManager.flagsB[i]}");
        }
        Debug.Log("��");
        Debug.Log(" ");
        Debug.Log(" ");
        */




    }
    public void resetcolor()
    {
        foreach (Flag flag in GameObject.FindObjectsOfType<Flag>()) //Flag �޸� ������Ʈ ����
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
        Ray ray = Camera.main.ScreenPointToRay(mousePos); //ī�޶󿡼� �ٷν��� ray ��

        RaycastHit hitinfo;//�������� �ޱ� 

        if(Physics.Raycast(ray, out hitinfo))//����ĳ��Ʈ���� �浹�� ������ Ŭ����
        {
            Debug.Log(hitinfo.transform.name+" : "+ hitinfo.point);
            _pickpos = hitinfo.point;
            return true;
        }
        
        //���õȰ� ������ ó��
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
