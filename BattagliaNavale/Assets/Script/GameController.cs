﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region fields

        public static int naviP1 = 0;
        public static int naviP2 = 0;
        public static bool startP1 = false;
        public static bool startP2 = false;
        public static bool iniziogioco = false;
        public static bool turno;

        #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        naviP1 = 0;
        naviP2 = 0;
        startP1 = false;
        startP2 = false;
        iniziogioco = false;
    }

    private void Awake()
    {
        naviP1 = 0;
        naviP2 = 0;
        startP1 = false;
        startP2 = false;
        iniziogioco = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}