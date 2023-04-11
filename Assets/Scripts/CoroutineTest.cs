using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestCoroutine());  //内风凭 角青内靛
        StartCoroutine("TestCoroutine2");
    }

   private IEnumerator TestCoroutine()
    {
        while (true)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(1f);
            Debug.Log("2");
            yield return new WaitForSeconds(1f);
            Debug.Log("3");
            yield return new WaitForSeconds(1f);
        }

    }
    private IEnumerator TestCoroutine2()
    {
        while (true)
        {
            Debug.Log("------1");
            yield return new WaitForSeconds(1f);
            Debug.Log("------2");
            yield return new WaitForSeconds(1f);
            Debug.Log("------3");
            yield return new WaitForSeconds(1f);
        }

    }

}
