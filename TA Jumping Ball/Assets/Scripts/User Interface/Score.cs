using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _textField;

    public void SetScore(int value)
    {
        _textField.text = value.ToString();
    }
}