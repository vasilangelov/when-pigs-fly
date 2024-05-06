using UnityEngine;

public enum JumpAction
{
    Jump = 0,
    DoubleJump = 1,
}

public static class AnimationIdentifiers
{
    public const string VelocityScalar = "ScVelocity";

    public const string FlapWingsTrigger = "TrFlapWings";
}

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;

    private JumpAction? _lastJumpAction = null;

    public Rigidbody _rigidbody;
    public Animator _animator;
    public GameManager _gameManager;

    public float _movementSpeed = 3f;
    public float _rotationSpeed = 180f;
    public float _jumpForce = 5f;
    public float _doubleJumpForce = 3f;
    public float _slipperyMultiplier = .25f;

    private void Start()
    {
        this._initialPosition = GlobalUtilities.CloneVector3(this.transform.position);
        this._initialRotation = GlobalUtilities.CloneVector3(this.transform.rotation.eulerAngles);
    }

    private void Update()
    {
        bool isGrounded = Physics.Raycast(this.transform.position, Vector3.down, GlobalConstants.GroundDistanceThreshold, GlobalConstants.LayerMasks.Ground);

        this.Move();

        bool isOnSlipperyGround = Physics.Raycast(this.transform.position, Vector3.down, GlobalConstants.GroundDistanceThreshold, GlobalConstants.LayerMasks.SlipperyGround);

        if (isOnSlipperyGround)
        {
            Vector3 flatForce = this._rigidbody.velocity.normalized.magnitude > .2f ? GlobalUtilities.CloneVector3(this._rigidbody.velocity.normalized) : this.transform.forward.normalized;
            flatForce.y = 0;

            this._rigidbody.AddForce(flatForce * (this._movementSpeed * this._slipperyMultiplier));
        }

        this.RotateCamera();

        if (Input.GetButtonDown(GlobalConstants.ButtonNames.Jump))
        {

            if (isGrounded || isOnSlipperyGround)
            {
                this.Jump(JumpAction.Jump);
            }
            else if (this._lastJumpAction == JumpAction.Jump)
            {
                this.Jump(JumpAction.DoubleJump);
            }
        }

        if (this.HasFallenOffMap())
        {
            this._gameManager.ResetCollectedCarrots();
            this.ResetPosition();
        }
    }

    private void Move()
    {
        float verticalInput = Input.GetAxis(GlobalConstants.MovementAxes.Vertical);

        Vector3 velocity = this.transform.forward * (verticalInput * this._movementSpeed);

        this._rigidbody.AddForce(velocity);

        this.LimitSpeed();

        this._animator.SetFloat(AnimationIdentifiers.VelocityScalar, Mathf.Abs(this._rigidbody.velocity.x + this._rigidbody.velocity.z));
    }

    private void LimitSpeed()
    {
        Vector3 plainVelocity = GlobalUtilities.CloneVector3(this._rigidbody.velocity);
        plainVelocity.y = 0;

        if (plainVelocity.magnitude > this._movementSpeed)
        {
            Vector3 limitedVelocity = plainVelocity.normalized * this._movementSpeed;
            limitedVelocity.y = this._rigidbody.velocity.y;

            this._rigidbody.velocity = limitedVelocity;
        }
    }

    private void RotateCamera()
    {
        float horizontalInput = Input.GetAxis(GlobalConstants.MovementAxes.Horizontal);
        transform.Rotate(0, horizontalInput * this._rotationSpeed * Time.deltaTime, 0);
    }

    private void Jump(JumpAction jumpAction)
    {
        float jumpForce = 0f;

        if (jumpAction == JumpAction.Jump)
        {
            jumpForce = this._jumpForce;
        }

        if (jumpAction == JumpAction.DoubleJump)
        {
            jumpForce = this._doubleJumpForce;
        }

        this._animator.SetTrigger(AnimationIdentifiers.FlapWingsTrigger);
        this._rigidbody.velocity = new Vector3(this._rigidbody.velocity.x, 0, this._rigidbody.velocity.z);
        this._rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        this._lastJumpAction = jumpAction;
    }

    private bool HasFallenOffMap() => this.transform.position.y <= GlobalConstants.OffMapYValue;

    private void ResetPosition()
    {
        this.transform.position = GlobalUtilities.CloneVector3(this._initialPosition);
        this.transform.rotation = Quaternion.Euler(this._initialRotation);
    }
}
