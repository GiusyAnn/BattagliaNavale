using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    #region Public Fields
    
        public Text turno;
        public Text numeroturno;
        public Text playerturno;
        public Text startgame;
        float tm = 300;

        public Slider sliderPlayer1;
        public Slider sliderPlayer2;
        
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        numeroturno.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        //Se il gioco è iniziato, faccio comparire le scritte in merito al turno
        if (GameController.iniziogioco)
        {
            turno.GetComponent<Text>().enabled = true;
            playerturno.GetComponent<Text>().enabled = true;
            //textiniziogioco();
        }

        //Modifico il contenuto delle scritte in base al giocatore che le visualizza
        if (PhotonNetwork.IsMasterClient)
        {
            //Modifico il numero dei turni
            numeroturno.GetComponent<Text>().enabled = true;
            numeroturno.text = " " + GameController.numeroTurnoPub;

            //A seconda del turno, setto la scritta al ogni giocatore
            if (GameController.turnoPub)
            {
                playerturno.text = "è il tuo turno";
            }
            else
            {
                playerturno.text = "è il turno dell'avversario";
            }

            sliderPlayer1.value = GameController.nv1;
            sliderPlayer2.value= GameController.naviaffondate2;
        }
        else
        {
            //Modifico il numero dei turni
            numeroturno.GetComponent<Text>().enabled = true;
            numeroturno.text = " " + GameController.numeroTurnoPub;


            //A seconda del turno, setto la scritta al ogni giocatore
            if (!GameController.turnoPub)
            {
                playerturno.text = "è il tuo turno";
            }
            else
            {
                playerturno.text = "è il turno dell'avversario";
            }
            
            sliderPlayer1.value = GameController.nv1;
            sliderPlayer2.value= GameController.naviaffondate2;
        }
    }
}
