
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

/// <summary>
/// Campo di immersione in cui inseriamo il nome del giocatore per farlo comparire su di esso nel gioco
/// </summary>
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants
    // Memorizziamo la chiave PlayerPrefab
    const string playerNamePrefKey = "PlayerName";
    #endregion

    #region MonoBehaviour CallBacks

    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;
        InputField input_filed = this.GetComponent<InputField>();
        if (input_filed != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                input_filed.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Setto il nome del player e lo salvo in PlayerPrefs per una futura sessione.
    /// </summary>
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("PlayerName è vuoto");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

    }
}