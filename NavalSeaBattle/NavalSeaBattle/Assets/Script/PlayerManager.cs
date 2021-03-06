﻿
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{

    #region Public Fields

        [Tooltip("Prefabricato del UI del giocatore")]
        [SerializeField]
        public GameObject PlayerUIPrefab;

        public GameObject casellaPlayer;

        public Camera mycam;
        public AudioListener myal;
        
        PhotonView pv;
        int x, y;
        
        [Tooltip("Istanza del giocatore Locale, per sapere se il giocatore è rappresentato nella scena")]
        public static GameObject LocalPlayerIstance;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Istanzio L'interfaccia del Player1
        if (PlayerUIPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUIPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab non è presente.", this);
        }

        pv = GetComponent<PhotonView>();
        if(pv.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                #region Tavole di Gioco
                
                //Istanziamo la Prima Tabella di Gioco
                for (x = 1; x < 11; x++)
                {
                    for (y = 1; y < 11; y++)
                    {
                        casellaPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Casella"),
                            new Vector3(-500 + (x * 90), 1, -1000 + (y * 90)), Quaternion.identity, 0);

                        setCasella(casellaPlayer, x, y, 1);

                        Tavola.table1Player1[x, y] = casellaPlayer.GetComponent<Casella>();
                    }
                }



                //Istanziamo la Seconda tabella di Gioco
                for (int x = 1; x < 11; ++x)
                {
                    for (int y = 1; y < 11; ++y)
                    {
                        casellaPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Casella"),
                            new Vector3(-500 + (x * 90), 1, 0 + (y * 90)), Quaternion.identity, 0);

                        setCasella(casellaPlayer, x, y, 2);
                        
                        Tavola.table2Player1[x, y] = casellaPlayer.GetComponent<Casella>();
                   }
                }
                
                #endregion
            }
        }
        else
        {
            //Facciamo in modo che il Player utilizzi la Camera che ha istanziato come figlio
            Destroy(mycam);
            Destroy(myal);
        }

    }

    //Prendiamo lo script presente nel GameObject Casella che abbiamo istanziato e vi inseriamo le informazioni di base
    public void setCasella (GameObject casellaPlayer, int x, int y, int numtable)
    {
        casellaPlayer.GetComponent<Casella>().SetRiga(x);
        casellaPlayer.GetComponent<Casella>().SetColonna(y);
        casellaPlayer.GetComponent<Casella>().SetTable(numtable);
        casellaPlayer.GetComponent<Casella>().SetPlayerTable(1);
    }

    void Awake()
    {
        //teniamo traccia dell'istanza del giocatore Locale per impedire la creazione di istanze quando i livelli sono sincronizzati
        if(photonView.IsMine)
        {
            PlayerManager.LocalPlayerIstance = this.gameObject;

            //contrassegniamo come non distruggere al caricamento in modo che l'istanza sopravviva alla sincronizzazione dei livelli,
            //offrendo così un'esperienza senza interruzioni quando i livelli vengono caricati.
            DontDestroyOnLoad(this.gameObject);
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

    }

    void CalledOnLevelWasLoaded(int level)
    {
        //Settiamo il NickName sull'interfaccia
        GameObject _uiGo = Instantiate(this.PlayerUIPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }
}
