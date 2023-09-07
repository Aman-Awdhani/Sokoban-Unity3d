using UnityEngine;
using DG.Tweening;

public struct BlockedBy
{
    public bool blocked;
    public bool blockedByBox;
}

public class PlayerMovement : MonoBehaviour , IMoveable
{
    public int movesCount;
   
    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5)//block diagonal move
            direction.x = 0;
        else
            direction.y = 0;

        direction.Normalize(); // Move only 1 unit

        Vector2 movePos = (Vector2) transform.position + direction;

        BlockedBy blockedBy = IsBlocked(transform.position, direction);

        if (blockedBy.blockedByBox)
        {
            if (blockedBy.blocked)
            {
                Debug.Log("Is Blocked");
                return false;
            }
            else
            {
                Debug.Log("Is Moving but with box");
                DoMove(movePos);
                movesCount++;
            }
        }
        else
        {
            if (blockedBy.blocked)
            {
                Debug.Log("Is Blocked");
                return false;
            }
            else
            {
                Debug.Log("Is Moving but NO box");
                DoMove(movePos);
                movesCount++;
            }
        }
        return true;
    }

    void DoMove(Vector2 movePos)
    {
        transform.DOMove(movePos, GameManager.Instance.moveSpeed).OnComplete(() => {
            GameManager.Instance.IsLevelComplete();
            GameManager.Instance.ChangeGameState(GameState.Idle);
        });
    }

    public BlockedBy IsBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        BlockedBy blocked;

        foreach (var wall in GameManager.Instance.spawnedElements.walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                blocked.blocked = true;
                blocked.blockedByBox = false;
                return blocked;
            }
        }

        foreach (var box in GameManager.Instance.spawnedElements.boxes)
        {
            if (box.gameobject.transform.position.x == newPos.x && box.gameobject.transform.position.y == newPos.y)
            {
                Box theBox = box.obj as Box;
                
                if (theBox && theBox.Move(direction))
                {
                    blocked.blocked = false;
                    blocked.blockedByBox = true;
                    return blocked;
                }
                else
                {
                    blocked.blocked = true;
                    blocked.blockedByBox = true;
                    return blocked;
                }
            }
        }
        blocked.blocked = false;
        blocked.blockedByBox = false;
        
        return blocked;
    }
}
