using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

    private PlayerManager target;
    private PlayerManagerP2 target2;

    #endregion


    #region MonoBehaviour Callbacks

    void Update()
    {

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

