using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _moves;
    [SerializeField] private TextMeshProUGUI _level;


    private void Start()
    {
        EventManager.Instance.onPlayerMove += UpdateMove;
        EventManager.Instance.onLevelComplete += LevelComplete;

    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayerMove -= UpdateMove;
        EventManager.Instance.onLevelComplete -= LevelComplete;

    }

    internal void UpdateMove(int move)
    {
        _moves.text = "Moves: " + move.ToString();
    }

    internal void UpdateLevel(int level)
    {
        _level.text = level.ToString();
    }

    internal void LevelComplete()
    {
        Debug.Log("Level Complete");
    }

}
