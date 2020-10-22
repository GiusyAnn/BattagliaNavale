using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace BattagliaNavale
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        ///<summary>
        ///definiamo il numero massimo di giocatori, così che quando una stanza sarà piena, ne sarà creata un'altra
        ///</summary>
        [Tooltip("definiamo il numero massimo di giocatori, così che quando una stanza sarà piena, ne sarà creata un'altra")]
        [SerializeField]
        private byte maxPlayerForRoom = 2;
        #endregion

        #region Private Fields
        /// <summary>
        /// Numero di versione del client. Gli utenti sono separati da gameVersion (che consente di apportare modifiche)
        /// </summary>
        string gameVersion = "1";

        [Tooltip("Panrel UI per il nome dell'utente")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("Panel UI per informare l'utente della connessione")]
        [SerializeField]
        private GameObject progressLabel;

        /// <summary>
        /// Teniamo traccia del processo in corso. Essendo la connessione asincrona è basata su differenti Callbacks
        /// Dobbiamo tenere traccia delle Callbacks per regolare correttamente il comportamento quando riceviamo una chiamata
        /// In generale viene utilizzato per il Callback OnConnectionToMaster()
        /// </summary>
        bool isConnecting;

        #endregion

        #region MonoBehaviour CallBacks
        /// <summary>
        /// Chiamato sui GameObject da Unity durante la fase iniziale di Inizializzazione
        /// </summary>
        void Awake()
        {
            //possiamo usare la chiamata sul ClientMaster e tutti i client nella stanza sincronizzano automaticamente il loro livello
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        #endregion

        // Update is called once per frame
        void Update()
        {

        }

        #region Public Metods
        ///<summary>
        ///Iniziamo il processo di connessione: Se siamo già connessi proviamo ad entrare in una stanza casuale, altrimenti connetti quest'istanza a Photon Cloud Network
        ///</summary>
        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                //Tentiamo di entrare in una stanza casuale, se falliamo riceviamo una notifica in OnJoinRandomFailed() e ne creiamo una
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                //dobbiamo connetterci a Photon Online Server.
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }

            controlPanel.SetActive(false);
            progressLabel.SetActive(true);
        }
        #endregion

        #region MonoBehaviourPunCallbacks Callbacks
        public override void OnConnectedToMaster()
        {
            //base.OnConnectedToMaster();
            Debug.Log("PUN/Launcher: OnConnectionToMaster() è stato chiamato dalla PUN");
            if(isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
            
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            //base.OnDisconnected(cause);
            Debug.Log("PUN/Launcher: OnDisconnected() è stato chiamato dalla PUN con la causa {0}");

            controlPanel.SetActive(true);
            progressLabel.SetActive(false);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            //base.OnJoinRandomFailed(returnCode, message);
            Debug.Log("PUN/Launcher: OnJoinRandomFailed() è stata chiamata dal PUN, non vi erano stanze disponibili, ne abbiamo creata un'altra chimando PhotonNetwork.CreateRoom");
            //non siamo riusciti ad unirci ad una stanza, forse non ne esisteva una oppure erano piene. Ne creiamo una
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayerForRoom });
        }

        public override void OnJoinedRoom()
        {
            //base.OnJoinedRoom();
            Debug.Log("PUN/Launcher: OnJoinedRoom() è stata chiamata dal PUN. ora questo client è nella Room");
            Debug.Log("Ci Siamo Uniti alla stanza Room For 1");
            //carichiamo la stanza
            PhotonNetwork.LoadLevel("Room for 1");
        }
        #endregion
    }
}
