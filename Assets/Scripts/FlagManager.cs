using System.Collections.Generic;

public class FlagManager
{
 
    public static List<Flag> PathFinding(Flag _entryFlag, Flag _goalFlag) //만들기 시작지점 끝지점플래그
    {
        List<Flag> result = new List<Flag>();  //빈리스트
        result.Add(_entryFlag); //시작 지점 리스트 추가
        Flag[] flag = _entryFlag.GetNextFlags();
        
    

        return result;
    }
    
}
