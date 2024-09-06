using UnityEngine;

namespace PlayerContent
{
    public class CameraMovement : MonoBehaviour
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
    
        [SerializeField] private float _mouseSensivity = 100f;
        [SerializeField] private Transform _playerBody;

        private float _xRotation = 0f;
        private float _mouseX = 0f;
        private float _mouseY = 0f;
        private float _angle = 65;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            _mouseX = Input.GetAxis(MouseX) * _mouseSensivity * Time.deltaTime;
            _mouseY = Input.GetAxis(MouseY) * _mouseSensivity * Time.deltaTime;
        
            const float deadZone = 0.01f;
            
            if (Mathf.Abs(_mouseX) < deadZone)
                _mouseX = 0;
            if (Mathf.Abs(_mouseY) < deadZone)
                _mouseY = 0;

            if (_mouseX == 0 || _mouseY == 0)
                return;
        
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -_angle, _angle);
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * _mouseX);
        }
    }
}