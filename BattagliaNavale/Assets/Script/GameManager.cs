using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Public Fields


    [Tooltip("Prefab da usare per istanziare le piattaforme di gioco")]
    GameObject playerPrefab;
    GameObject playerPrefab2;
    public static Casella[,] globalTable = new Casella[11,11];
    
    #endregion

    #region Photon Callbacks
    /// <summary>
    /// vengono chiamate quando il giocatore locale lascia la stanza. Dobbiamo caricare la scena Luncher.
    /// </summary>
    public override void OnLeftRoom()
    {
        //base.OnLeftRoom();
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        //base.OnPlayerEnteredRoom(other);
        Debug.LogFormat("OnPlayerEnteredRoom() {0} ", other.NickName);

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }
    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Private Methods

    void LoadArena()
    {
        
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork: Ho provato ad entrare in un livello ma non sono il MasterClient ");
        }
        Debug.LogFormat("PhotonNetwork: Livello Caricato : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

        #region Istantiate Player
        if (PhotonNetwork.IsMasterClient)
        {
            // siamo in una stanza. genera un personaggio per il giocatore locale. viene sincronizzato utilizzando PhotonNetwork.Instantiate
            playerPrefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Plane"), new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            DontDestroyOnLoad(playerPrefab);
           
        }
        else
        {
            // siamo in una stanza. genera un personaggio per il giocatore locale. viene sincronizzato utilizzando PhotonNetwork.Instantiate
            playerPrefab2 = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlanePlayer2"), new Vector3(1200f, 0f, 0f), Quaternion.identity, 0);
            DontDestroyOnLoad(playerPrefab);
            
        }
        #endregion
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
