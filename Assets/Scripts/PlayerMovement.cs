using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] JoystickController _joystickController;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontal = _joystickController.Horizontal();
        float vertical = _joystickController.Vertical();

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement != Vector3.zero)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, movement, _rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        SetAnimation(movement);
        transform.Translate(movement * _movementSpeed * Time.deltaTime, Space.World);
    }

    void SetAnimation(Vector3 movement)
    {
        _playerAnimator.SetFloat("horizontalAnimations", movement.x);
        _playerAnimator.SetFloat("verticalAnimations", movement.z);
    }
}