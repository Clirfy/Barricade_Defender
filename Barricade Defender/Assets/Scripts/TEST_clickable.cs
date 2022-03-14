using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_clickable : MonoBehaviour
{
    public GameObject SkillPanel;

    private void OnMouseDown()
    {
        SkillPanel.SetActive(!SkillPanel.activeSelf);
    }
}
