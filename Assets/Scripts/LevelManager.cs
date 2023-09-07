using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData //Single level
{
    public List<string> rows = new List<string>(); //rows of text define level

    public int Height
    {
        get
        {
            return rows.Count; //Height of Level is defined by number of rows of text file
        }
    }
    public int Width
    {
        get
        {
            int maxLength = 0;
            foreach (var row in rows)
            {
                if (row.Length > maxLength)
                {
                    maxLength = row.Length;// Longest line defines width of level so we need to find the max and return it
                }
            }
            return maxLength;
        }
    }
}

public class LevelManager : MonoBehaviour
{
    public string _file;
    public List<LevelData> _levels;

    private void Awake()
    {
        LoadLevelsFromFile();
    }

    private void LoadLevelsFromFile()
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
        _levels.Add(new LevelData());

        for (long i = 0; i < lines.LongLength; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                _levels.Add(new LevelData());
                continue;
            }
            _levels[_levels.Count - 1].rows.Add(line);
        }
    }

}
