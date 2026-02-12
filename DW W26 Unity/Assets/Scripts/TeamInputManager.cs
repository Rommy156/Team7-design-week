using UnityEngine;
using UnityEngine.InputSystem;

public class TeamInputManager : MonoBehaviour
{
    public TeamPlayerController teamPlayer;

    private int joinIndex;

    public void OnPlayerJoined(PlayerInput input)
    {
        joinIndex++;

        if (joinIndex == 1)
        {
            Debug.Log("Movement player assigned");
            teamPlayer.AssignMovementPlayer(input);
        }
        else if (joinIndex == 2)
        {
            Debug.Log("Shooter player assigned");
            teamPlayer.AssignShooterPlayer(input);
        }
    }
}
