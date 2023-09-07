using UnityEngine;

public interface IMoveable
{
    public bool Move(Vector2 direction);

    public BlockedBy IsBlocked(Vector3 position, Vector2 direction);


}
