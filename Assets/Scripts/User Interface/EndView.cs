using UnityEngine;
using UnityEngine.UI;

public class EndView : MonoBehaviour
{
    [SerializeField] private Text _endTextField;
    [SerializeField] private string _winText;
    [SerializeField] private string _lossText;

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void ShowWinText()
    {
        _endTextField.text = _winText;
        ShowView();
    }

    public void ShowLossText()
    {
        _endTextField.text = _lossText;
        ShowView();
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }
}