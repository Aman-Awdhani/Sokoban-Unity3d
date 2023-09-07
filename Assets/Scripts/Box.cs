using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour , IMoveable
{
    public bool arrived;

    public bool Move(Vector2 direction)
    {

        Vector2 movePos = (Vector2)transform.position + direction;


        BlockedBy blockedBy = IsBlocked(transform.position, direction);
        if (blockedBy.blocked || blockedBy.blockedByBox)
        {
            return false;
        }
        else
        {
            ArrivedOnCross(direction);
            transform.DOMove(movePos, GameManager.Instance.moveSpeed);

            Debug.Log("Move the Box");

            return true;
        }
    }
    
    public BlockedBy IsBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        
        BlockedBy blockedBy = new BlockedBy();

        foreach (var wall in GameManager.Instance.spawnedElements.walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                Debug.Log("Blocked by Wall");
                blockedBy.blocked = true;
                return blockedBy;
            }
        }

        foreach (var box in GameManager.Instance.spawnedElements.boxes)
        {
            if (box.gameobject.transform.position.x == newPos.x && box.gameobject.transform.position.y == newPos.y)
            {
                Debug.Log("Blocked by another Box");
                blockedBy.blocked = true;
                blockedBy.blockedByBox = true;

                return blockedBy;
            }
        }
        return blockedBy;
    }

    void ArrivedOnCross(Vector2 direction)
    {
        //SpriteRenderer boxColor = GetComponent<SpriteRenderer>();
        foreach(var slot in GameManager.Instance.spawnedElements.slots)
        {
            if(transform.position.x + direction.x == slot.transform.position.x && transform.position.y + direction.y == slot.transform.position.y)
            {
                //boxColor.color = Color.green;
                arrived = true;
                return;
            }
        }
        //boxColor.color = Color.white;
        arrived = false;
    }
}
