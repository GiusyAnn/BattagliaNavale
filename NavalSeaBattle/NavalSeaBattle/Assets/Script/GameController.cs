using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson.PunDemos;

[RequireComponent(typeof(PhotonView))]
public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    #region fields

        public static int naviP1 = 0;
        public static int naviP2 = 0;
        public  bool startP1 = false;
        public  bool startP2 = false;
        public static bool iniziogioco = false;
        public int n2=0;
        public int n1=0;
        public  int numeroTurno = 1;
        public static int numeroTurnoPub = 1;
        public static bool turnoPub = true;
        public bool turno = true;
        public static int nv1 = 0;
        public static int naviaffondate2 = 0;
        private PhotonView pv;

        #endregion
    
    

    void Start()
    {
        pv = GetComponentInParent<PhotonView>();
        naviP1 = 0;
        naviP2 = 0;
        startP1 = false;
        startP2 = false;
        iniziogioco = false;
        turno = true;
        turnoPub = turno;
        numeroTurno = 1;
        numeroTurnoPub = numeroTurno;
        nv1 = 0; 
        naviaffondate2 = 0;
    }

    //Risetto i parametri iniziali nel caso il gioco venga riavviato
    public void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        naviP1 = 0;
        naviP2 = 0;
        startP1 = false;
        startP2 = false;
        iniziogioco = false;
        turno = true;
        turnoPub = turno;
        numeroTurno = 1;
        numeroTurnoPub = numeroTurno;
        nv1 = 0; 
        naviaffondate2 = 0;
    }

    #region PunRPC Metods

    //Il Player1 è pronto setta a true il suo booleano
    [PunRPC]
    public void gioco1(bool si)
    {
        this.startP1 = si;
        Debug.Log("Ho avviato la funzione INIZIO GIOCO 1, il valore di startP1 è : "+startP1+ "  P2StartP1 : "+startP2);
    }
    
    //Il Player2 è pronto e setta a true il suo booleano
    [PunRPC]
    public  void gioco2(bool si)
    {
        this.startP2 = si;
        Debug.Log("Ho avviato la funzione INIZIO GIOCO 2, il valore di startP1 è : "+startP1+ "  P2StartP1 : "+startP2); 
    }
    
    //Setto il turno per il Player1
    [PunRPC]
    public void setTurnoP1()
    {
        this.turno = true;
        this.numeroTurno = this.numeroTurno + 1;
    }

    //Setto il turno per il Player2
    [PunRPC]
    public void setTurnoP2()
    {
        this.turno = false;
        this.numeroTurno = this.numeroTurno + 1;
    }

    [PunRPC]
    public void SettaNavi1()
    {
        this.n1 = this.n1 + 1;
        Debug.LogWarning("SONO NALLA PUN SETTO NAVI 1 :le navi sono "+ n1);
    }

    [PunRPC]
    public void Navi2()
    {
        this.n2 = this.n2 + 1;
        Debug.LogWarning("SONO NALLA PUN SETTO NAVI 2 :le navi sono "+ n2);
    }
    
    #endregion

    public void setgioco1()
    {
        pv.RPC("gioco1", RpcTarget.All, true);
    }

    public void setgioco2()
    {
        pv.RPC("gioco2",RpcTarget.All, true);
    }

    // Update is called once per frame
    void Update()
    {

           if (PhotonNetwork.IsMasterClient)
        {
            if (startP1 == true && startP2 == true)
            {
                //entremabi i giocatori hanno cliccato su "Gioca" quindi sono pronti ad iniziare
                iniziogioco = true;
            }
            
            //SINCRONIZZO I TURNI DEI GIOCATORI
            //quando un giocaore ha giocato, ha modificato la sua variabile publica di turno, quindi risulterà diversa da qualle locale condivisa con PUN
            if (numeroTurno < numeroTurnoPub)
            {
                //a seconda del numero del tunrno scopro se è il turno del giocatore 1 o quello del giocatore 2, e mi chiamo la funzione PunRPC
                int res = numeroTurnoPub % 2;
                if (res == 0)
                {
                    pv.RPC("setTurnoP2", RpcTarget.All);
                    numeroTurnoPub = numeroTurno;
                    turnoPub = turno;
                }
                else
                {
                    pv.RPC("setTurnoP1", RpcTarget.All);
                    numeroTurnoPub = numeroTurno;
                    turnoPub = turno;
                }
            }
            else if (numeroTurnoPub < numeroTurno)
            {
                //Se l'altro giocatore ha modificato il turno, la variabile locale PUN sarà diversa, quindi setto i miei valori uguali e aggiorno il turno
                numeroTurnoPub = numeroTurno;
                turnoPub = turno;
            }

            if (n1 != nv1 || n2 != naviaffondate2)
            {
                //Controllo che il numero locale e il numero PUN di navi del Player1 sia uguale e nal caso lo modifico
                if (n1 < nv1)
                {
                    pv.RPC("SettaNavi1", RpcTarget.All);
                    nv1 = n1;
                    Debug.LogWarning("SONO USCITA DALLA PUN SETTO NAVI 1 :le navi sono "+ n1 +" le PUB sono : "+nv1);
                } else if (nv1 < n1)
                {
                    nv1 = n1;
                }
            
                //Controllo che il numero locale e il numero PUN di navi del Player2 sia uguale e nal caso lo modifico
                if (n2 < naviaffondate2)
                {
                    pv.RPC("Navi2", RpcTarget.All);
                    naviaffondate2 = n2;
                } else if (naviaffondate2 < n2)
                {
                    naviaffondate2 = n2;
                }
            }
        }
        else
        {
            if (startP1 == true && startP2 == true)
            {
                //entremabi i giocatori hanno cliccato su "Gioca" quindi sono pronti ad iniziare
                iniziogioco = true;
            }
            
            //SINCRONIZZO I TURNI DEI GIOCATORI
            //quando un giocaore ha giocato, ha modificato la sua variabile publica di turno, quindi risulterà diversa da qualle locale condivisa con PUN
            if (numeroTurno < numeroTurnoPub)
            {
                //a seconda del numero del tunrno scopro se è il turno del giocatore 1 o quello del giocatore 2, e mi chiamo la funzione PunRPC
                int res = numeroTurnoPub % 2;
                if (res == 0)
                {
                    pv.RPC("setTurnoP2", RpcTarget.All);
                    numeroTurnoPub = numeroTurno;
                    turnoPub = turno;
                }
                else
                {
                    pv.RPC("setTurnoP1", RpcTarget.All);
                    numeroTurnoPub = numeroTurno;
                    turnoPub = turno;
                }
            }
            else if (numeroTurnoPub < numeroTurno)
            {
                //Se l'altro giocatore ha modificato il turno, la variabile locale PUN sarà diversa, quindi setto i miei valori uguali e aggiorno il turno
                numeroTurnoPub = numeroTurno;
                turnoPub = turno;
            }
            
            if (n1 != nv1 || n2 != naviaffondate2)
            {
                //Controllo che il numero locale e il numero PUN di navi del Player1 sia uguale e nal caso lo modifico
                if (n1 < nv1)
                {
                    pv.RPC("SettaNavi1", RpcTarget.All);
                    nv1 = n1;
                    Debug.LogWarning("SONO USCITA DALLA PUN SETTO NAVI 1 :le navi sono "+ n1 +" le PUB sono : "+nv1);
                } else if (nv1 < n1)
                {
                    nv1 = n1;
                }
            
                //Controllo che il numero locale e il numero PUN di navi del Player2 sia uguale e nal caso lo modifico
                if (n2 < naviaffondate2)
                {
                    pv.RPC("Navi2", RpcTarget.All);
                    naviaffondate2 = n2;
                } else if (naviaffondate2 < n2)
                {
                    naviaffondate2 = n2;
                }
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Possediamo questo giocatore: invia queste informazioni agli altri i nostri dati
            stream.SendNext(startP1);
            stream.SendNext(startP2);
            stream.SendNext(iniziogioco);
            stream.SendNext(n1);
            stream.SendNext(n2);
        }
        else
        {
            //Lettore di rete, ricevi dati
            startP1 = (bool)stream.ReceiveNext();
            startP2 = (bool)stream.ReceiveNext();
            iniziogioco = (bool)stream.ReceiveNext();
            n1 = (int)stream.ReceiveNext();
            n2 = (int)stream.ReceiveNext();
        }
    }

   
}
