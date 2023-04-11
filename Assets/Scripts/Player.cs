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
        //Debug.Log(Input.mousePosition);     //���콺 ������Ȯ��
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
}
