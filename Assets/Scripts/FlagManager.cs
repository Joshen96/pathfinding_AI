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
    static List<Flag> passedProcessedList = new List<Flag>(); //백트래킹을 이용해서 골인지점까지 간 플래그 리스트담기용
    static List<float> passedcostList = new List<float>(); // 백트래킹을 이용해서 골인지점을 갈때마다 플래그마다의 거리를 리스트로 담기용

    static List<Flag[]> resultsList = new List<Flag[]>(); // 골인지점까지 간 경로들의 리스트 (일단 골인까지 가는 모든경우의수)
    static List<float> resultscostList = new List<float>(); // 골인지점까지 간 경로들의 리스트의 총거리합을 담은 리스트
    public static List<Flag> PathFinding(Flag _entryFlag, Flag _goal)
    {
        //bool isGoal =false;
        passedProcessedList.Clear();
        passedcostList.Clear();
        resultsList.Clear();
        resultscostList.Clear();
        // Debug.Log(passedProcessedList.Count);
        passedProcessedList.Add(_entryFlag);
        FlagSearch(_entryFlag, _goal); //재귀

        //모든 경로 및 비용 계산완료

        //모든경로 비교하여 최소 찾기
        float distance = float.MaxValue; int shortest = -1;
        for (int i = 0; i < resultsList.Count; ++i)
        {
            float resultDistance = resultscostList[i]; // resultsList[i]의 길이를 계산할 것

            string t = "";
            for (int j = 0; j < resultsList[i].Length; j++)
            {
                t += resultsList[i][j] + " - ";

            }
            Debug.Log("경로" + t);

            Debug.Log($"비용 : {resultDistance} ({distance})");

            if (distance > resultDistance)
            {
                distance = resultDistance;
                shortest = i;
            }
        }
        if (shortest < 0)
        {
            Debug.Log("길을 못 찾음!");
            return new List<Flag>();
        }

        foreach(Flag flag in resultsList[shortest].ToList())
        {
            flag.SetColor(Flag.EState.Selected);
        }

        return resultsList[shortest].ToList(); //최소비용 경로 배출
    }

    // 백트래킹 사용
    static private void FlagSearch(Flag _current, Flag _goal)
    {
        if (_current == _goal) //목표만나면
        {
            resultsList.Add(passedProcessedList.ToArray()); //경로 넣기 (경로 모음집) //경로넣기
            resultscostList.Add(passedcostList.Sum()); //cost 넣기      (비용 모음집)
            
            
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
