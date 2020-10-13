using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Casella : MonoBehaviour, IPunObservable
{

    #region Public Fields
        public int table;
        public int riga;
        public int colonna;
        public bool naveposizionataP1;
        public bool colpitaP1;
        public bool naveposizionataP2;
        public bool colpitaP2;
        public bool affondatadaP1;
        public bool affondatadaP2;
        public int player;

        public Texture2D mirino;
        public Texture2D mouse;


    #endregion

    #region Public Method

    public void SetTable(int num)
    {
        this.table = num;
    }

    public void SetRiga (int num)
    {
        this.riga = num;
    }

    public void SetColonna (int num)
    {
        this.colonna = num;
    }

    public int GetRiga()
    {
        return this.riga;
    }

    public int GetColonna()
    {
        return this.colonna;
    }

    public void PosizionaNaveP1()
    {
        this.naveposizionataP1 = true;
    }

    public bool IsPosizionataP1()
    {
        return naveposizionataP1;
    }

    public void ColpisciP1 ()
    {
        this.colpitaP1 = true;
    }

    public bool IsColpitaP1()
    {
        return colpitaP1;
    }

    public void PosizionaNaveP2()
    {
        this.naveposizionataP2 = true;
    }

    public bool IsPosizionataP2()
    {
        return naveposizionataP1;
    }

    public void ColpisciP2()
    {
        this.colpitaP2 = true;
    }

    public bool IsColpitaP2()
    {
        return colpitaP2;
    }

    public void AffondaP1()
    {
        this.affondatadaP1 = true;
    }

    public bool IsAffondataP1()
    {
        return affondatadaP1;
    }

    public void AffondaP2()
    {
        this.affondatadaP2 = true;
    }

    public bool IsAffondataP2()
    {
        return affondatadaP2;
    }

    public void SetPlayerTable (int num)
    {
        this.player = num;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Mouse Functions

    //facciamo in modo che quando il Mouse passi sopra le caselle, esso divento un mirino
    private void OnMouseEnter()
    {
        Cursor.SetCursor(mirino, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
    }

    #endregion
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Possediamo questo giocatore: invia queste informazioni agli altri i nostri dati
            stream.SendNext(table);
            stream.SendNext(riga);
            stream.SendNext(colonna);
            stream.SendNext(naveposizionataP1);
            stream.SendNext(naveposizionataP2);
            stream.SendNext(colpitaP1);
            stream.SendNext(colpitaP2);
            stream.SendNext(affondatadaP1);
            stream.SendNext(affondatadaP2);
            stream.SendNext(player);
        }
        else
        {
            //Lettore di rete, ricevi dati
            this.table = (int)stream.ReceiveNext();
            this.riga = (int)stream.ReceiveNext();
            this.colonna = (int)stream.ReceiveNext();
            this.naveposizionataP1 = (bool)stream.ReceiveNext();
            this.naveposizionataP2 = (bool)stream.ReceiveNext();
            this.colpitaP1 = (bool)stream.ReceiveNext();
            this.colpitaP2 = (bool)stream.ReceiveNext();
            this.affondatadaP1 = (bool)stream.ReceiveNext();
            this.affondatadaP2 = (bool)stream.ReceiveNext();
            this.player = (int)stream.ReceiveNext();
        }
    }
}
