using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Skins : MonoBehaviour
{
    [SerializeField]
    public List<Skin> _skins = new List<Skin>();
    public GameObject _prefabSkinCell;
}
