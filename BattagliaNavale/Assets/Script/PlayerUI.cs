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


    [Tooltip("UI Slider per la salute del giocatore")]
    [SerializeField]
    private Slider playerHealthSlider;

    private PlayerManager target;


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

    #endregion


}

