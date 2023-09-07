using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    internal int currentLevel;

    [SerializeField]
    internal List<LevelElement> levelElements;

    private LevelData _level;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    GameObject GetPrefab(char c)
    {
        LevelElement element = levelElements.Find(le => le.character == c.ToString());

        if (element == null) return null;

        return element.prefab;
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel >= GetComponent<LevelManager>()._levels.Count)
        {
            currentLevel = 0; 
        }
    }

    public void BuildLevel()
    {
        _level = _levelManager._levels[currentLevel];

        int startX = -_level.Width / 2; 
        int x = startX;
        int y = -_level.Height / 2;

        foreach (var row in _level.rows)
        {
            foreach (var c in row)
            {
                GameObject prefab = GetPrefab(c);

                if (prefab)
                {
                    GameObject element = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                    GameManager.Instance.spawnedElements.AddElements(element);
                }

                x++;
            }
            y++; //new line
            x = startX;//reset x
        }
    }
}
