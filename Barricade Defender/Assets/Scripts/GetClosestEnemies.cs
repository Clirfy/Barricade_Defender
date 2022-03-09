using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetClosestEnemies : MonoBehaviour
{
    public int TargetAmmount;

    public string TargetTag;

    private GameObject[] TargetsFoundArray;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetNearest(TargetTag);
        }
    }

    private void GetNearest(string targetTag)
    {
        GameObject[] test = new GameObject[5];
        TargetsFoundArray = GameObject.FindGameObjectsWithTag(targetTag);
        TargetsFoundArray.OrderBy(o => Vector2.Distance(o.transform.position, transform.position)).ToArray();
        test[0] = TargetsFoundArray.OrderBy(o => Vector2.Distance(o.transform.position, transform.position)).ToArray()[0];

        //for (int i = 0; i < TargetAmmount; i++)
        //{
        //    Debug.Log("target " + (i + 1) + ": " + TargetsFoundArray[i].name);
        //    //Debug.Log(TargetsFoundArray.OrderBy(o => Vector2.Distance(o.transform.position, transform.position)).FirstOrDefault());
        //}     
        Debug.Log(test[0].name);
    }
}
