using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelManager : MonoBehaviour
{
    public GameObject OpenPanelButton;
    public GameObject HelpPanel;


    public void OpenPanel()
    {
        HelpPanel.SetActive(true);
        OpenPanelButton.SetActive(false);
    }

    public void ClosePanel()
    {
        HelpPanel.SetActive(false);
        OpenPanelButton.SetActive(true);
    }
}
