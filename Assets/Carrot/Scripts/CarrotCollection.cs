using UnityEngine;

public class CarrotCollection : MonoBehaviour
{
    private GameManager _gameManager;

    public string _carrotId;

    private void Awake()
    {
        this._gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player))
        {
            this._gameManager.CollectCarrot(this._carrotId);

            this.gameObject.SetActive(false);
        }
    }
}
