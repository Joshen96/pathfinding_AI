using System.Collections.Generic;

public class FlagManager
{
 
    public static List<Flag> PathFinding(Flag _entryFlag, Flag _goalFlag) //����� �������� �������÷���
    {
        List<Flag> result = new List<Flag>();  //�󸮽�Ʈ
        result.Add(_entryFlag); //���� ���� ����Ʈ �߰�
        Flag[] flag = _entryFlag.GetNextFlags();
        
    

        return result;
    }
    
}
