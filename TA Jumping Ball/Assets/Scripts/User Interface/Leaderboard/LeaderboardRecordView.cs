using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRecordView : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _valueText;

    public void SetRecord(LeaderboardRecord record)
    {
        _nameText.text = record.Name;
        _valueText.text = record.Value.ToString();
    }
}