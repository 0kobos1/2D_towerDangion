using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    // ���͂��i�[���郊�X�g
    [SerializeField] List<string> lines;

    public List<string> Lines { get => lines; }
}
