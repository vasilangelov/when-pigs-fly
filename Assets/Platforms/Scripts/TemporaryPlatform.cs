using System;

using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{
    private DateTime? _initialStepTime = null;
    private Color _initialColor;

    private Renderer _renderer;
    private Material _material;
    private Collider _collider;

    public float _checkDistance = .85f;
    public float _msWarn = 2500;
    public float _msDisable = 5000;
    public float _msEnable = 10000;
    public Color _warningColor = Color.black;

    private void Awake()
    {
        this._renderer = this.GetComponent<Renderer>();
        this._material = this._renderer.material;
        this._collider = this.GetComponent<Collider>();

        this._initialColor = new Color(this._material.color.r, this._material.color.g, this._material.color.b);
    }

    public void Update()
    {
        if (this._initialStepTime == null)
        {
            bool hasPlayerStepped = Physics.CheckSphere(this.transform.position, this._checkDistance, GlobalConstants.LayerMasks.Player);

            if (hasPlayerStepped)
            {
                this._initialStepTime = DateTime.Now;
            }

            return;
        }

        TimeSpan difference = DateTime.Now - this._initialStepTime.Value;

        if (difference.TotalMilliseconds >= this._msWarn)
        {
            this._material.color = this._warningColor;
        }

        if (difference.TotalMilliseconds >= this._msDisable)
        {
            this._collider.enabled = false;
            this._renderer.enabled = false;
        }

        if (difference.TotalMilliseconds >= this._msEnable)
        {
            this._material.color = new Color(this._initialColor.r, this._initialColor.g, this._initialColor.b);
            this._collider.enabled = true;
            this._renderer.enabled = true;
            this._initialStepTime = null;
        }
    }
}
