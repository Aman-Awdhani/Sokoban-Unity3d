
using UnityEngine;


public class PlayerInput : MonoBehaviour
{

    [SerializeField] private PlayerMovement _player;
    private bool isMoving;
    void Update()
    {

        if (!GameManager.Instance.AllowInput()) return; // only true when button pressed once

        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // read keyboard input
        if (movementInput.magnitude == 0) return;

        movementInput.Normalize();

        print(movementInput);

        if (_player)
        {
            if (movementInput.sqrMagnitude > 0.5) // check pressed button to move once at a time
            {
                isMoving = _player.Move(movementInput);

                if (!isMoving)
                    GameManager.Instance.ChangeGameState(GameState.Idle);
                else
                {
                    EventManager.Instance.onPlayerMove?.Invoke(GameStats.Instance.IncreaseMoveCount());
                    GameManager.Instance.ChangeGameState(GameState.Moving);
                }
            }
            else
            {
                GameManager.Instance.ChangeGameState(GameState.Idle);
            }
        }
    }
}
