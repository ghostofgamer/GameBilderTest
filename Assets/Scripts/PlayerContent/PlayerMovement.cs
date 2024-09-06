using UnityEngine;

namespace PlayerContent
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed = 13f;
        
        private Vector3 _move;
        private float _x;
        private float _z;
        
        private void Update()
        {
            _x = Input.GetAxis(Horizontal);
            _z = Input.GetAxis(Vertical);
            _move = transform.right * _x + transform.forward * _z;
            _controller.Move(_move * _speed * Time.deltaTime);
        }
    }
}