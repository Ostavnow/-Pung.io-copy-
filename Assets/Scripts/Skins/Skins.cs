using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
[Serializable]
[RequireComponent(typeof(SkinsEditorData))]
[RequireComponent(typeof(SkinUI))]
public class Skins : MonoBehaviour
{
    public List<Skin> _listSkins = new List<Skin>();
}