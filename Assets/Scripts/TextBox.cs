using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TMP_Text _textbox;
    [SerializeField] private List<string> _dialogue = new();
    [SerializeField] private UnityEvent _onComplete;
    private int _curTextIdx = 0;

    void Start()
    {
        _textbox.text = _dialogue[0];
    }

    public void IncrementText()
    {
        _curTextIdx++;
        if(_curTextIdx >= _dialogue.Count)
        {
            this.gameObject.SetActive(false);
            _onComplete.Invoke();
            return;
        }
        _textbox.text = _dialogue[_curTextIdx];
    }
}
