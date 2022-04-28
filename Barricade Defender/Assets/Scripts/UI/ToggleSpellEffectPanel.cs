using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSpellEffectPanel : MonoBehaviour
{
    public GameObject SpellEffetPanel;

    public void TogglePanel()
    {
        SpellEffetPanel.SetActive(!SpellEffetPanel.activeSelf);
    }
}
