using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class endgame : MonoBehaviour
{
    #region Public Fields

    public Text scritta;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (GameController.nv1 == 10)
            {
                scritta.text = "Hai Perso!";
            } else if (GameController.naviaffondate2 == 10)
            {
                scritta.text = "Hai Vinto!";
            }
        }
        else
        {
            if (GameController.nv1 == 10)
            {
                scritta.text = "Hai Vinto!";
            } else if (GameController.naviaffondate2 == 10)
            {
                scritta.text = "Hai Perso!";
            }
        }
        
    }
}
