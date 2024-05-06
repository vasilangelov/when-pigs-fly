using UnityEngine;

public class PlayerWinCondition : MonoBehaviour
{
    public GameManager _gameManager;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.Tags.Player))
        {
            if (this._gameManager.IsWinConditionMet())
            {
                this._gameManager.ShowWinMessage();
            }
            else
            {
                this._gameManager.ShowRequirementMessage();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.Tags.Player))
        {
            this._gameManager.HideCenterMessage();
        }
    }
}
