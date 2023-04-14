using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_event : MonoBehaviour
{
    private Button btn = null;
    
    public Player player = null;
    public GameObject target = null;
    public Flag Flag = null;

    public Flag testFlag = null;

    private List<Flag> flagList = null;
    private void Awake()

    {
        
        //player = GetComponent<Player>();
        btn = GetComponent<Button>();
    }
    public void pathck()
    {
        player.resetcolor();
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<Player>();

        Debug.Log("asd" + player.GetLastFLag());
            GameObject parent = transform.parent.gameObject;
            UI_Flag getflag = parent.GetComponent<UI_Flag>();

        player.SetFlag(Flag);
        
        flagList = FlagManager.PathFinding(player.GetLastFLag(), Flag);
        foreach ( Flag flag in flagList ) {
            Debug.Log(flagList);
        }
        
        player.SetPathList(flagList);
        

        
        
            


    }
    


}
