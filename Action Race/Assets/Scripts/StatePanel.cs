using UnityEngine;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button startGameButton;
    [SerializeField] Button stopGameButton;
    [SerializeField] Button pauseGameButton;

    public void ConfigureAccess(bool isMasterClient, State state = State.Stop)
    {
        if (isMasterClient)
        {
            switch(state)
            {
                case State.Play:
                    startGameButton.gameObject.SetActive(false);
                    stopGameButton.gameObject.SetActive(true);
                    pauseGameButton.gameObject.SetActive(false);
                    break;

                case State.Stop:
                    startGameButton.gameObject.SetActive(true);
                    stopGameButton.gameObject.SetActive(false);
                    pauseGameButton.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
            stopGameButton.gameObject.SetActive(false);
            pauseGameButton.gameObject.SetActive(false);
        }
    }
}
