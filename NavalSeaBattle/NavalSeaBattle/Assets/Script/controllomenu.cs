using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllomenu : MonoBehaviour
{
    public Canvas menu;
    public Canvas tutorial;
        public void activeMenu()
        {
            menu.GetComponent<Canvas>().enabled = true;
            tutorial.GetComponent<Canvas>().enabled = false;
        }

    public void activeTutorial()
    {
        menu.GetComponent<Canvas>().enabled = false;
        tutorial.GetComponent<Canvas>().enabled = true;
    }
}
