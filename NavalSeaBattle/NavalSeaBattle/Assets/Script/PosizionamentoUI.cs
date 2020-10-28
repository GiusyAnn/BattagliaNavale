using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PosizionamentoUI : MonoBehaviour
{
    public  Text naviP1;
    public  Text naviP2;
    public  Text player1;
    public  Text player2;
    public  Button bottone1;
    public Button bottone2;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player2.GetComponent<Text>().enabled = false;
            naviP2.GetComponent<Text>().enabled = false;

        }
        else
        {
            player1.GetComponent<Text>().enabled = false;
            naviP1.GetComponent<Text>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            naviP1.text = " " + GameController.naviP1;
            if (GameController.naviP1 == 10)
            {
                bottone1.GetComponent<Image>().enabled = true;
                bottone1.GetComponentInChildren<Text>().enabled = true;
            }
        }
        else
        {
            naviP2.text = " " + GameController.naviP2;
            if (GameController.naviP2 == 10)
            {
                bottone2.GetComponent<Image>().enabled = true;
                bottone2.GetComponentInChildren<Text>().enabled = true;
            }
        }
    }

    #region MyRegion

    //i giocatori hanno posizionato le loro navi e sono pronti ad iniziare la partita
        public  void inizioGioco1()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (GameController.naviP1 == 10 )
                {
                    player1.text = "Attendi che l'altro giocatore sia pronto ad iniziare ...";
                    naviP1.text = " ";
                    bottone1.GetComponent<Image>().enabled = false;
                    bottone1.GetComponentInChildren<Text>().enabled = false;
                } 
            }
            else
            {
                if (GameController.naviP2 == 10)
                {
                    player2.text = "Attendi che l'altro giocatore sia pronto ad iniziare ...";
                    naviP2.text = " ";
                    bottone2.GetComponent<Image>().enabled = false;
                    bottone2.GetComponentInChildren<Text>().enabled = false;
                }
            }
        }
        
    
    #endregion
    
   

    
}
