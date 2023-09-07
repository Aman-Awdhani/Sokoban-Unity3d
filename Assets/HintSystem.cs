using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HintData
{
    public List<Vector2> directions = new List<Vector2>();
}

public class HintSystem : MonoBehaviour
{
    public string _file;

    [SerializeField] private List<HintData> _hints;


    private void Awake()
    {
        LoadHintsFromFile();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        ShowHint();
    //}

    private void LoadHintsFromFile()
    {
        TextAsset text = (TextAsset)Resources.Load(_file);
        if (!text)
        {
            Debug.Log("Levels file:" + _file + ".txt does not exist!");
            return;
        }
        else
        {
            Debug.Log("Levels imported!");
        }

        string _levelsText = text.text;
        string[] lines;

        lines = _levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None); //splitting on new line
        _hints.Add(new HintData());


        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("New level added");
                _hints.Add(new HintData());
                continue;
            }

            string[] dir = line.Split(",");

            Vector2 direction = new Vector2(int.Parse(dir[0].ToString()), int.Parse(dir[1].ToString()));

            _hints[_hints.Count - 1].directions.Add(direction);
        }
    }

    private void ShowHint()
    {
        GameManager.Instance.ResetScene();
        StartCoroutine(UseHint());
    }

    IEnumerator UseHint()
    {
        int currentLevel = GameStats.Instance.currentLevel;

        for (int i = 0; i < _hints[currentLevel].directions.Count; i++)
        {
            GameManager.Instance._player.Move(_hints[currentLevel].directions[i]);
            yield return new WaitForSeconds(GameManager.Instance.moveSpeed);
        }
    }

}
