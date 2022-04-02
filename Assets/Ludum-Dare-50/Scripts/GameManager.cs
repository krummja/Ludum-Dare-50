using UnityEngine;


public class GameManager : BaseManager<GameManager>
{
    public Player Player;

    private void Update()
    {
        PlayerCharacterInputs inputs = InputManager.Instance.Inputs;
        Player.TryMove(inputs.MoveInput);
    }

    protected override void OnAwake() {}
}
