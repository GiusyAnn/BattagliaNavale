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
        
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            //Controllo se il Player2 ha posizionato una nave, e modifico anche la mia tavola
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    //Player1 - Posizoionamento Nave
                    //Controllo se i valori coincidono
                    if (Tavola.table1Player2[i, j].naveposizionataP2 !=
                        Tavola.table2Player1[i, j].naveposizionataP2)
                    {
                        //Controllo se il Player2 ha posizonato una nave e l'aggiungo anche al Player1
                        if(Tavola.table1Player2[i,j].naveposizionataP2)
                            Tavola.table2Player1[i,j].PosizionaNaveP2();
                        
                    }
                    
                    //Player2 - Posizonamento Nave
                    //Controllo se i valori coincidono
                    if (Tavola.table1Player1[i, j].naveposizionataP1 !=
                        Tavola.table2Player2[i, j].naveposizionataP1)
                    {
                        //Controllo se il Player1 ha posizonato una nave e l'aggiungo anche al Player1
                        if(Tavola.table1Player1[i,j].naveposizionataP1)
                            Tavola.table1Player2[i,j].PosizionaNaveP1();
                        
                    }
                }
            }
            
            
        }
    }
    
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Possediamo questo giocatore: invia queste informazioni agli altri i nostri dati
            stream.SendNext(table2Player1);
            stream.SendNext(table2Player2);
            stream.SendNext(table1Player1);
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