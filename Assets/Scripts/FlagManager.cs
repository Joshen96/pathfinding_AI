using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class FlagManager
{
    static List<Flag> passedProcessedList = new List<Flag>(); //��Ʈ��ŷ�� �̿��ؼ� ������������ �� �÷��� ����Ʈ����
    static List<float> passedcostList = new List<float>(); // ��Ʈ��ŷ�� �̿��ؼ� ���������� �������� �÷��׸����� �Ÿ��� ����Ʈ�� ����

    static List<Flag[]> resultsList = new List<Flag[]>(); // ������������ �� ��ε��� ����Ʈ (�ϴ� ���α��� ���� ������Ǽ�)
    static List<float> resultscostList = new List<float>(); // ������������ �� ��ε��� ����Ʈ�� �ѰŸ����� ���� ����Ʈ
    public static List<Flag> PathFinding(Flag _entryFlag, Flag _goal)
    {
        //bool isGoal =false;
        passedProcessedList.Clear();
        passedcostList.Clear();
        resultsList.Clear();
        resultscostList.Clear();
        // Debug.Log(passedProcessedList.Count);
        passedProcessedList.Add(_entryFlag);
        FlagSearch(_entryFlag, _goal); //���

        //��� ��� �� ��� ���Ϸ�

        //����� ���Ͽ� �ּ� ã��
        float distance = float.MaxValue; int shortest = -1;
        for (int i = 0; i < resultsList.Count; ++i)
        {
            float resultDistance = resultscostList[i]; // resultsList[i]�� ���̸� ����� ��

            string t = "";
            for (int j = 0; j < resultsList[i].Length; j++)
            {
                t += resultsList[i][j] + " - ";

            }
            Debug.Log("���" + t);

            Debug.Log($"��� : {resultDistance} ({distance})");

            if (distance > resultDistance)
            {
                distance = resultDistance;
                shortest = i;
            }
        }
        if (shortest < 0)
        {
            Debug.Log("���� �� ã��!");
            return new List<Flag>();
        }

        foreach(Flag flag in resultsList[shortest].ToList())
        {
            flag.SetColor(Flag.EState.Selected);
        }

        return resultsList[shortest].ToList(); //�ּҺ�� ��� ����
    }

    // ��Ʈ��ŷ ���
    static private void FlagSearch(Flag _current, Flag _goal)
    {
        if (_current == _goal) //��ǥ������
        {
            resultsList.Add(passedProcessedList.ToArray()); //��� �ֱ� (��� ������) //��γֱ�
            resultscostList.Add(passedcostList.Sum()); //cost �ֱ�      (��� ������)
            
            
            return;
        }

        //
        foreach (Flag _canGoList in _current.GetNextFlags())
        {
            if (passedProcessedList.Contains(_canGoList)) continue;

            passedProcessedList.Add(_canGoList);
            passedcostList.Add(Vector3.Distance(_current.gameObject.transform.position, _canGoList.gameObject.transform.position));

            FlagSearch(_canGoList, _goal);

            passedProcessedList.Remove(_canGoList);
            passedcostList.Remove(Vector3.Distance(_current.gameObject.transform.position, _canGoList.gameObject.transform.position));
        }

        /*
        Flag[] canGoList = _current.GetNextFlags();

        for (int i = 0; i < canGoList.Length; ++i)
        {
            if (passedProcessedList.Contains(canGoList[i])) continue;

            passedProcessedList.Add(canGoList[i]);

            //passedcostList.Add(_current.getDis(canGoList[i]));
            passedcostList.Add(Vector3.Distance(_current.gameObject.transform.position, canGoList[i].gameObject.transform.position));


            FlagSearch(canGoList[i], _goal);

            passedProcessedList.Remove(canGoList[i]);
            passedcostList.Remove(Vector3.Distance(_current.gameObject.transform.position, canGoList[i].gameObject.transform.position));
            //passedcostList.Remove(_current.getDis(canGoList[i]));
        
        }
        */


        //


        Flag[] canGoList = _current.GetNextFlags();

        for (int i = 0; i < canGoList.Length; ++i)
        {
            if (passedProcessedList.Contains(canGoList[i])) continue;

            passedProcessedList.Add(canGoList[i]);

            //passedcostList.Add(_current.getDis(canGoList[i]));
            passedcostList.Add(Vector3.Distance(_current.gameObject.transform.position, canGoList[i].gameObject.transform.position));


            FlagSearch(canGoList[i], _goal);

            passedProcessedList.Remove(canGoList[i]);
            passedcostList.Remove(Vector3.Distance(_current.gameObject.transform.position, canGoList[i].gameObject.transform.position));
            //passedcostList.Remove(_current.getDis(canGoList[i]));
        }



    }

}
