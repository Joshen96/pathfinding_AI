using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager
{
    // Dijkstra algorithm
    public static List<Flag> PathFinding(Flag _entryFlag, Flag _goalFlag)
    {
        Dictionary<Flag, float> distance = new Dictionary<Flag, float>();  //(키값)플래그와 (벨류)거리float 
        Dictionary<Flag, Flag> previous = new Dictionary<Flag, Flag>();    //(키값)플래그와 (벨류)플래그  
        List<Flag> unvisited = new List<Flag>();                           //플래그 리스트 방문

        distance[_entryFlag] = 0;
        previous[_entryFlag] = null;
        //시작 거리 딕셔려리
        //시작 플래그 

        foreach (Flag flag in GameObject.FindObjectsOfType<Flag>()) //Flag 달린 오브젝트 전부
        {
            if (flag != _entryFlag) //시작플래그 제외하고
            {
                distance[flag] = Mathf.Infinity; //시작을 제외한 나머지 거리 무한대 지정ㄴ
                previous[flag] = null;           //null
            }

            unvisited.Add(flag); //방문안한 리스트에 넣음 
        }

        while (unvisited.Count > 0)
        {
            Flag currentFlag = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Flag flag in unvisited)
            {
                if (distance[flag] < shortestDistance)
                {
                    shortestDistance = distance[flag];
                    currentFlag = flag;
                }
            }

            if (currentFlag == _goalFlag)
            {
                break;
            }

            unvisited.Remove(currentFlag); //이때 뺴고  //시작을뺴고
            
            //뺸걸가지고
            foreach (Flag neighbor in currentFlag.GetNextFlags()) //방문한 플래그의 다음 플래그 전부검사
            {                                                                         //
                float altDistance = distance[currentFlag] + Vector3.Distance(currentFlag.transform.position, neighbor.transform.position);
                // 다음플래그들과 방문플래그의 거리를 구하여 altDistance에 넣고
                if (altDistance < distance[neighbor])  
                {
                    distance[neighbor] = altDistance;  //다음갈곳들의 길이를 넣어줌 (지금까지온길이+갈길이)
                    previous[neighbor] = currentFlag; //전의 플래그


                }
            }
        }

        List<Flag> path = new List<Flag>();
        Flag current = _goalFlag;

        while (current != null)
        {
            path.Insert(0, current); // 0번째 요소에 삽입 역순으로 뱉음
            current = previous[current];
        }

        for (int i = 0; i < path.Count; i++)
        {
            path[i].SetColor(Flag.EState.Selected);
            if (i > 0) path[i].gameObject.SetActive(true);
        }

        return path;
    }


    /*
    // A* algorithm
    public static List<Flag> PathFinding(Flag _entryFlag, Flag _goalFlag)
    {
        Dictionary<Flag, Flag> cameFrom = new Dictionary<Flag, Flag>();
        Dictionary<Flag, float> costSoFar = new Dictionary<Flag, float>();
        List<Flag> frontier = new List<Flag>();

        frontier.Add(_entryFlag);
        cameFrom[_entryFlag] = null;
        costSoFar[_entryFlag] = 0f;

        while (frontier.Count > 0)
        {
            Flag current = frontier[0];
            frontier.RemoveAt(0);

            if (current == _goalFlag)
                break;

            Flag[] neighbors = current.GetnextFlag();
            foreach (Flag next in neighbors)
            {
                float newCost = costSoFar[current] + Vector3.Distance(current.transform.position, next.transform.position);
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    float priority = newCost + Vector3.Distance(next.transform.position, _goalFlag.transform.position);
                    int index = frontier.FindIndex(x => priority < costSoFar[x] + Vector3.Distance(x.transform.position, _goalFlag.transform.position));
                    if (index >= 0)
                    {
                        frontier.Insert(index, next);
                    }
                    else
                    {
                        frontier.Add(next);
                    }
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(_goalFlag))
        {
            Debug.LogError("Path not found");
            return null;
        }

        List<Flag> path = new List<Flag>();
        Flag currentFlag = _goalFlag;
        while (currentFlag != _entryFlag)
        {
            path.Insert(0, currentFlag);
            currentFlag.SetColor(Flag.EState.Selected);
            currentFlag = cameFrom[currentFlag];
        }
        path.Insert(0, _entryFlag);
        _entryFlag.SetColor(Flag.EState.Selected);

        return path;
    }
    */
}
