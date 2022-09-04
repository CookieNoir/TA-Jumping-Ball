using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private LeaderboardFileManager _fileManager;
    [SerializeField] private LeaderboardRecordView[] _recordViews;
    [SerializeField] private GameObject _inputView;
    [SerializeField] private GameObject _leaderboardView;
    private LeaderboardRecord[] _leaderboardRecords;
    private int _savedValue;
    private bool _set = false;

    public void TryToSetNewRecord(int value)
    {
        if (!_set)
        {
            _FillLeaderboardRecords();
            _set = true;
        }
        bool isRecord = value > _leaderboardRecords[^1].Value;
        _inputView.SetActive(isRecord);
        _leaderboardView.SetActive(!isRecord);
        _savedValue = isRecord ? value : 0;
    }

    public void SubmitRecord(string playerName)
    {
        LeaderboardRecord newRecord = new LeaderboardRecord(playerName, _savedValue);
        int position = _FindRecordPosition(newRecord);
        if (position < _leaderboardRecords.Length)
        {
            for (int i = _leaderboardRecords.Length - 2; i >= position; --i)
            {
                _leaderboardRecords[i + 1] = _leaderboardRecords[i];
            }
            _leaderboardRecords[position] = newRecord;
            _SaveLeaderboardRecords();
        }
        _RefreshRecordViews();
        _inputView.SetActive(false);
        _leaderboardView.SetActive(true);
    }

    private int _FindRecordPosition(LeaderboardRecord newRecord)
    {
        int position = _leaderboardRecords.Length;
        while (position > 0 && newRecord.CompareTo(_leaderboardRecords[position - 1]) > 0) position--;
        return position;
    }

    private void _FillLeaderboardRecords()
    {
        _leaderboardRecords = _fileManager.ReadRecords(_recordViews.Length);
        _RefreshRecordViews();
    }

    private void _RefreshRecordViews()
    {
        for (int i = 0; i < _leaderboardRecords.Length; ++i)
        {
            _recordViews[i].SetRecord(_leaderboardRecords[i]);
        }
    }

    private void _SaveLeaderboardRecords()
    {
        _fileManager.WriteRecords(_leaderboardRecords);
    }
}