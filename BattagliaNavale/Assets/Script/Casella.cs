using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casella : MonoBehaviour
{

    #region Public Fields
        public int table;
        public int riga;
        public int colonna;
        public bool naveposizionataP1;
        public bool colpitaP1;
        public bool naveposizionataP2;
        public bool colpitaP2;
        public bool affondatadaP1;
        public bool affondatadaP2;
        public int player;
    


    #endregion

    #region Public Method

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
        this.affondatadaP1 = true;
    }

    public bool IsAffondataP1()
    {
        return affondatadaP1;
    }

    public void AffondaP2()
    {
        this.affondatadaP2 = true;
    }

    public bool IsAffondataP2()
    {
        return affondatadaP2;
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
        
    }
}
