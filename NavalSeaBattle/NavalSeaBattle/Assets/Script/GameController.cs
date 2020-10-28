using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    #region fields

        public static int naviP1 = 0;
        public static int naviP2 = 0;
        public  bool startP1 = false;
        public  bool startP2 = false;
        public  bool iniziogioco = false;
        public  bool turno;
        public Canvas posizionanavi;

        private PhotonView pv;

        #endregion
    
    

    void Start()
    {
        pv = GetComponentInParent<PhotonView>();
        Debug.LogWarning("Ho Creato il GameController la sua PhotonView é : " + pv);
        naviP1 = 0;
        naviP2 = 0;
        startP1 = false;
        startP2 = false;
        iniziogioco = false;
    }

   

    #region PunRPC Metods

    //Il Player1 è pronto setta a true il suo booleano
    [PunRPC]
    public void gioco1(bool si)
    {
        this.startP1 = si;
        Debug.Log("Ho avviato la funzione INIZIO GIOCO 1, il valore di startP1 è : "+startP1+ "  P2StartP1 : "+startP2);
    }
    
    [PunRPC]
    public  void gioco2(bool si)
    {
        this.startP2 = si;
        Debug.Log("Ho avviato la funzione INIZIO GIOCO 2, il valore di startP1 è : "+startP1+ "  P2StartP1 : "+startP2); 
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
                //settiamo a true il primo turno, e a false il secondo e così via... 
                turno = true;
            }
            
            if (iniziogioco == true)
            {
                this.posizionanavi.GetComponent<Canvas> ().enabled = false;
            }
        }
        else
        {
            if (startP1 == true && startP2 == true)
            {
                //entremabi i giocatori hanno cliccato su "Gioca" quindi sono pronti ad iniziare
                iniziogioco = true;
                //settiamo a true il primo turno, e a false il secondo e così via... 
                turno = true;
            }
            
            if (iniziogioco == true)
            {
                posizionanavi.GetComponent<Canvas> ().enabled = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Possediamo questo giocatore: invia queste informazioni agli altri i nostri dati
            stream.SendNext(naviP1);
            stream.SendNext(naviP2);
            stream.SendNext(startP1);
            stream.SendNext(startP2);
            stream.SendNext(iniziogioco);
            stream.SendNext(turno);
        }
        else
        {
            //Lettore di rete, ricevi dati
            naviP1 = (int)stream.ReceiveNext();
            naviP2 = (int)stream.ReceiveNext();
            startP1 = (bool)stream.ReceiveNext();
            startP2 = (bool)stream.ReceiveNext();
            iniziogioco = (bool)stream.ReceiveNext();
            turno = (bool)stream.ReceiveNext();
        }
    } 
}
