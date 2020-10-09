using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region Private Fields


    [Tooltip("UI: Testo del Nome Player sul Display")]
    [SerializeField]
    private Text playerNameText;

    [Tooltip("UI: Testo del Nome Player sul Display")]
    [SerializeField]
    private Text playerNameText2;


    [Tooltip("UI Slider per la salute del giocatore")]
    [SerializeField]
    private Slider playerHealthSlider;

    private PlayerManager target;
    private PlayerManagerP2 target2;


    #endregion


    #region MonoBehaviour Callbacks

    void Update()
    {
        // Referenza alla salute del Player
        if (playerHealthSlider != null)
        {
            //playerHealthSlider.value = target.Health;
        }

        //Distruggi l'oggetto se il targher è vuoto
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }

     void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    #endregion


    #region Public Methods

    public void SetTarget(PlayerManager _target)
    {
        if(_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        target = _target;
        if (playerNameText != null)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }

    public void SetTarget2(PlayerManagerP2 pm2)
    {
        if (pm2 == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        target2 = pm2;
        if (playerNameText2 != null)
        {
            playerNameText2.text = target2.photonView.Owner.NickName;
        }
    }

    #endregion


}

