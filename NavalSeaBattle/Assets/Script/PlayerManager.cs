
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
            #region Tavole di Gioco

            if (PhotonNetwork.IsMasterClient)
            {
                //Istanziamo la Prima Tabella di Gioco
                for (x = 0; x < 7; x++)
                {
                    for (y = 0; y < 7; y++)
                    {

                        Debug.LogFormat("Istanziamo la casella della 1 tavola in riga " + x + " e colonna " + y);

                        casellaPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Casella"),
                            new Vector3(-300 + (x * 100), 1, -700 + (y * 100)), Quaternion.identity, 0);

                        setCasella(casellaPlayer, x, y, 1);



                    }
                }



                //Istanziamo la Seconda tabella di Gioco
                for (int x = 0; x < 7; ++x)
                {
                    for (int y = 0; y < 7; ++y)
                    {
                        Debug.LogFormat("Istanziamo la casella della 2 tavola in riga " + x + " e colonna " + y);

                        casellaPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Casella"),
                            new Vector3(-300 + (x * 100), 1, 100 + (y * 100)), Quaternion.identity, 0);

                        setCasella(casellaPlayer, x, y, 2);

                    }
                }
            }

            #endregion
        }
        else
        {
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
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }

        GameObject _uiGo = Instantiate(this.PlayerUIPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }
}
