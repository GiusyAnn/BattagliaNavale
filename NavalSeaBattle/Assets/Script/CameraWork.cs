using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    #region Private Fields
    [Tooltip("La distanza x-y dal bersaglio")]
    [SerializeField]
    private float distance = 20.0f;


    [Tooltip("l'altezza a cui vogliamo sia al di sopra")]
    [SerializeField]
    private float height = 890.0f;

    [Tooltip("consentire alla telecamera di essere spostata verticalmente rispetto all'obiettivo")]
    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;

    [Tooltip("Impostalo come falso se un componente di un prefabbricato viene istanziato da Photon Network e chiama manualmente OnStartFollowing () quando e se necessario.")]
    [SerializeField]
    public bool followOnStart = false;

    //trasformazione memorizzata nella cache del target
    Transform cameraTransform;

    // mantiene una bandiera internamente per riconnettersi se il bersaglio viene perso o la telecamera viene cambiata
    bool isFollowing;

    //cache per l'offset della cam
    Vector3 cameraOffset = Vector3.zero;

    #endregion

    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        // Inizia a seguire l'oggetto
        if(followOnStart)
        {
            OnStartFollowing();
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //// Il target di trasformazione potrebbe non distruggersi con un carico di livello,
        ///quindi dobbiamo coprire i casi d'angolo in cui la fotocamera principale è diversa ogni volta che cariciamo una nuova scena e ricollegarci quando ciò accade
        if(cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        if(isFollowing)
        {
            Follow();
        }
    }

    #endregion

    #region Public Methods
    /// Genera l'inizio dopo l'evento. usato quando non sai al momento della modifica cosa seguire.
    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        //non levigiamo nulla, andiamo direttamente alla ripresa della telecamera giusta
        Cut();
    }

    #endregion

    #region Private Methods

    //Seguiamo l'oggetto
    void Follow()
    {
        cameraOffset.x = -distance;
        cameraOffset.y = height;

        cameraTransform.LookAt(this.transform.position + centerOffset);
    }

    void Cut()
    {
        cameraOffset.x = -distance;
        cameraOffset.y = height;

        cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);

        cameraTransform.LookAt(this.transform.position + centerOffset);
    }

    #endregion

}
