using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Skins : MonoBehaviour
{
    [SerializeField]
    public List<Skin> skins = new List<Skin>();
    public GameObject prefabSkinCell;
}
