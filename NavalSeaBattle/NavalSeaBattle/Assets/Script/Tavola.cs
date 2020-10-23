using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Tavola : MonoBehaviour, IPunObservable
{
    #region Public Fields
    
    //Dichiaro le 4 tavole di gioco    
    public static Casella[,] table1Player1 = new Casella[11,11];
    public static Casella[,] table2Player1 = new Casella[11,11];
    public static Casella[,] table1Player2 = new Casella[11,11];
    public static Casella[,] table2Player2 = new Casella[11,11];

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Possediamo questo giocatore: invia queste informazioni agli altri i nostri dati
            stream.SendNext(table2Player1);
            stream.SendNext(table2Player2);
            stream.SendNext(table1Player1);
            stream.SendNext(table1Player2);
        }
        else
        {
            //Lettore di rete, ricevi dati
            Tavola.table1Player2 = (Casella[,])stream.ReceiveNext();
            Tavola.table2Player2 = (Casella[,])stream.ReceiveNext();
            Tavola.table1Player1 = (Casella[,])stream.ReceiveNext();
            Tavola.table2Player1 = (Casella[,])stream.ReceiveNext();
        }
    } 
    
    
}