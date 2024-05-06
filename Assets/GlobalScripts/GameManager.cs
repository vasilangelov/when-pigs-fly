using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly HashSet<string> _carrotsLeft = new();

    public CarrotCollection[] _carrots;
    public TMP_Text _text;
    public TMP_Text _messageText;

    private void Start()
    {
        ResetCollectedCarrots();
        HideCenterMessage();
    }

    public bool IsWinConditionMet() => this._carrotsLeft.Count == 0;

    public void ShowWinMessage()
    {
        this._messageText.SetText("You win!");
        this._messageText.gameObject.SetActive(true);
    }

    public void ShowRequirementMessage()
    {
        string carrot = this._carrotsLeft.Count > 1 ? "carrots" : "carrot";
        this._messageText.SetText($"You need to collect all {this._carrots.Length} carrots! {this._carrotsLeft.Count} {carrot} left.");
        this._messageText.gameObject.SetActive(true);
    }

    public void HideCenterMessage()
    {
        this._messageText.gameObject.SetActive(false);
    }

    public void CollectCarrot(string carrodId)
    {
        this._carrotsLeft.Remove(carrodId);

        UpdateCollectedCarrotsText();
    }

    public void ResetCollectedCarrots()
    {
        foreach (CarrotCollection carrot in this._carrots)
        {
            this._carrotsLeft.Add(carrot._carrotId);
            carrot.gameObject.SetActive(true);
        }

        UpdateCollectedCarrotsText();
    }

    private void UpdateCollectedCarrotsText()
    {
        this._text.SetText($"{this._carrots.Length - this._carrotsLeft.Count} / {this._carrots.Length}");
    }
}
