using System.Collections;
using UnityEngine;


public class GameManager : BaseManager<GameManager>
{
    public Player Player;
    public Clock Clock;

    private int currentMoves = 10;

    public void DecrementMoves()
    {
        currentMoves--;
        Clock.UpdateClock(false);
    }

    private void FixedUpdate()
    {
        PlayerCharacterInputs inputs = InputManager.Instance.Inputs;
        Player.TryMove(inputs.MoveInput);
    }

    protected override void OnAwake() {}
}
