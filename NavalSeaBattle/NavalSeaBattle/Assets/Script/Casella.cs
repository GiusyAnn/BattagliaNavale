
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Casella : MonoBehaviourPunCallbacks, IPunObservable
{

    #region Public Fields
        public int table;
        public int riga = 0;
        public int colonna = 0;
        public bool naveposizionataP1;
        public bool colpitaP1;
        public bool naveposizionataP2;
        public bool colpitaP2;
        public bool p2affondaP1;
        public bool p1affondaP2;
        public int player;
        
        public Texture2D mirino;
        public Texture2D mouse;
        public Material nave;
        public Material casella1;
        public Material casella2;
        public Material colpita;
        public Material affondata;

        private PhotonView pv;

        #endregion

    #region Public Method

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

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
        this.p2affondaP1 = true;
    }

    public bool IsAffondataP1()
    {
        return p2affondaP1;
    }

    public void AffondaP2()
    {
        this.p1affondaP2 = true;
    }

    public bool IsAffondataP2()
    {
        return p1affondaP2;
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
        //Modifichiamo l'aspetto delle caselle del Player1 nel caso esse vangano colpite e/o affondate
        if (table == 1 && player == 1)
        {
            if (p2affondaP1 && colpitaP1)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = affondata;
            } else if(colpitaP1)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = colpita;
            }
        }

        //Modifichiamo l'aspetto delle caselle del Player2 nel caso esse vangano colpite e/o affondate
        if (table == 1 && player == 2)
        {
            if (p1affondaP2 && colpitaP2)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = affondata;
            } else if (colpitaP2)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = colpita;
            }
        }
    }

 

    #region OnMouse Function
    
        #region Mouse Icon

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

    public void OnMouseDown()
    {
        //Operazioni del Giocatore 1
        #region Player1
        
            if (this.player == 1 )
            {
                //Posizionamento Nave
                if (GameController.naviP1 < 10 && this.naveposizionataP1 == false && this.table == 1)
                {
                    GameController.naviP1 = GameController.naviP1 + 1;
                    this.gameObject.GetComponent<MeshRenderer>().material = nave;
                    this.pv.RPC("PosizionaNaveP1",RpcTarget.All,riga,colonna);
                }
                
                //ProvaColpo
                if ( this.table == 2 && GameController.iniziogioco && GameController.turnoPub)
                {
                    //controllo se nella casella dove sto colpendo l'avversario ha posizionato una nave
                    if (naveposizionataP2)
                    {
                        this.gameObject.GetComponent<MeshRenderer>().material = affondata;
                        //devo effettuare le modifiche per il numero di navi e il turno
                        this.pv.RPC("p1affondanavedip2",RpcTarget.All,riga,colonna);
                        GameController.numeroTurnoPub = GameController.numeroTurnoPub + 1;
                        GameController.turnoPub = false;
                    }
                    else
                    {
                        this.gameObject.GetComponent<MeshRenderer>().material = colpita;
                        //devo effettuare le modifiche per il turno
                        this.pv.RPC("p1ColpisceCasellaP2",RpcTarget.All,riga,colonna);
                        GameController.numeroTurnoPub = GameController.numeroTurnoPub + 1;
                        GameController.turnoPub = false;
                    }
                }
            }
        
        #endregion

        //Operazioni del Giocatore 2
        #region Player2
           
            if (this.player == 2 )
            {
                //Posizionamento Nave 
                if ( GameController.naviP2<10 && this.naveposizionataP2 == false && this.table == 1 && GameController.iniziogioco == false)
                {
                    GameController.naviP2 = GameController.naviP2 + 1;
                    this.gameObject.GetComponent<MeshRenderer>().material = nave;
                    this.pv.RPC("PosizionaNaveP2",RpcTarget.All,riga,colonna);
                }
                
                //ProvaColpo
                if ( this.table == 2 && GameController.turnoPub == false && GameController.iniziogioco)
                {
                    //controllo se nella casella dove sto colpendo l'avversario ha posizionato una nave
                    if (naveposizionataP1)
                    {
                        this.gameObject.GetComponent<MeshRenderer>().material = affondata;
                        //devo effettuare le modifiche per il numero di navi e il turno
                        this.pv.RPC("p2affondanavedip1",RpcTarget.All,riga,colonna);
                        GameController.numeroTurnoPub = GameController.numeroTurnoPub + 1;
                        GameController.turnoPub = true;
                    }
                    else
                    {
                        this.gameObject.GetComponent<MeshRenderer>().material = colpita;
                        //devo effettuare le modifiche per il turno
                        this.pv.RPC("p2ColpisceCasellaP1",RpcTarget.All,riga,colonna);
                        GameController.numeroTurnoPub = GameController.numeroTurnoPub + 1;
                        GameController.turnoPub = true;
                    }
                }
            }

        #endregion
        
    }

    #endregion

    #region PunRPCMetods

    #region Posizionamento e Rimozione di una Nave

        //Il Player1 posiziona una nave
        [PunRPC]
        public void PosizionaNaveP1(int riga, int colonna)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Tavola.table1Player1[riga,colonna].PosizionaNaveP1();
            }
            else
            {
                Tavola.table2Player2[riga,colonna].PosizionaNaveP1();
            }
        }
        
        //Il Player 2 Posizona una nave
        [PunRPC]
        public void PosizionaNaveP2(int riga, int colonna)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Tavola.table2Player1[riga,colonna].PosizionaNaveP2();
            }
            else
            {
                Tavola.table1Player2[riga,colonna].PosizionaNaveP2();
            }
        }
        #endregion

        #region Colpisci e/o Affonda

        //Il Player 1 Affonda una Nave del Player2
        [PunRPC]
        public void p1affondanavedip2(int riga, int colonna)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Tavola.table2Player1[riga,colonna].AffondaP2();
                Tavola.table2Player1[riga,colonna].ColpisciP2();
            }
            else
            {
                Tavola.table1Player2[riga,colonna].AffondaP2();
                Tavola.table1Player2[riga,colonna].ColpisciP2();
            }
        }
        
        //Il Player 2 Affonda una Nave del Player1
        [PunRPC]
        public void p2affondanavedip1(int riga, int colonna)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Tavola.table1Player1[riga,colonna].AffondaP1();
                Tavola.table1Player1[riga,colonna].ColpisciP1();
            }
            else
            {
                Tavola.table2Player2[riga,colonna].AffondaP1();
                Tavola.table2Player2[riga,colonna].ColpisciP1();
            }
        }
        
        //Il Player 1 Colpisce
        [PunRPC]
        public void p1ColpisceCasellaP2(int riga, int colonna)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Tavola.table2Player1[riga,colonna].ColpisciP2();
            }
            else
            {
                Tavola.table1Player2[riga,colonna].ColpisciP2();
            }
        }
        
        //Il Player 2 Colpisce 
        [PunRPC]
        public void p2ColpisceCasellaP1(int riga, int colonna)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Tavola.table1Player1[riga,colonna].ColpisciP1();
            }
            else
            {
                Tavola.table2Player2[riga,colonna].ColpisciP1();
            }
        }

    #endregion

    #endregion

    #region PhotonView Observer

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
                stream.SendNext(p1affondaP2);
                stream.SendNext(p2affondaP1);
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
                this.p1affondaP2 = (bool)stream.ReceiveNext();
                this.p1affondaP2 = (bool)stream.ReceiveNext();
                this.player = (int)stream.ReceiveNext();
            }
        }

    #endregion
   
}
