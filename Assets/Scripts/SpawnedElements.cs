using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ElementInfo
{
    public Vector2 initPos;
    public object obj;
    public GameObject gameobject;
}

public class SpawnedElements : MonoBehaviour
{
    [SerializeField] internal List<GameObject> walls = new List<GameObject>();
    [SerializeField] internal List<ElementInfo> boxes = new List<ElementInfo>();
    [SerializeField] internal List<GameObject> slots = new List<GameObject>();

    internal ElementInfo player = new ElementInfo();

    public void AddElements(GameObject element)
    {
        if (element == null) return;

        if (element.CompareTag("Wall"))
        {
            walls.Add(element);
        }

        else if (element.CompareTag("Box"))
        {
            ElementInfo info = new ElementInfo();
            info.gameobject = element;
            info.obj = element.GetComponent<Box>();
            info.initPos = element.gameObject.transform.position;
            boxes.Add(info);
        }

        else if (element.CompareTag("Slot"))
            slots.Add(element);

        else if (element.CompareTag("Player"))
        {
            player.gameobject = element;
            player.obj = element.GetComponent<PlayerMovement>();
            player.initPos = element.transform.position;
        }
    }   
}
