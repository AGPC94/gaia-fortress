﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite sprMark;
    [TextArea(1, 10)]
    public string[] sentences;
    public float timeToAppear;
    public float timeToDisappear;


}
