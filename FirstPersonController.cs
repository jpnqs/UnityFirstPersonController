using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPersonController
{

    public class FirstPersonController : MonoBehaviour
    {

        private CameraController _cameraController;
        private CharacterController _characterController;

        public GameObject _camera;
        public float _movementSpeed;
        public float _sprintMultiplier;
        public float _crouchMultiplier;
        public float _laidMultiplier;
        private GameObject _controller;

        public KeyCode _moveForward = KeyCode.W;
        public KeyCode _moveLeft = KeyCode.A;
        public KeyCode _moveBackward = KeyCode.S;
        public KeyCode _moveRight = KeyCode.D;
        public KeyCode _sprint = KeyCode.LeftShift;
        public KeyCode _crouch = KeyCode.C;
        public KeyCode _laid = KeyCode.Y;
        public KeyCode _jump = KeyCode.Space;

        private void Awake()
        {
            _controller = this.gameObject;
            _characterController = new CharacterController( _controller, 
                                                            _camera, 
                                                            _movementSpeed, 
                                                            _sprintMultiplier, 
                                                            _crouchMultiplier, 
                                                            _laidMultiplier );
        }

        private void Update()
        {
            if (_characterController.axes == CharacterController.RotationAxis.MouseX)
            {
                _characterController.cameraMovementX();
                _characterController.cameraMovementY();
            }
        }

        private void FixedUpdate()
        {
            int keyCount = 0;
            bool isSprinting = false;
            bool isCrouching = false;
            bool isLaid = false;

            if (Input.GetKeyDown(this._jump))
            {
                this._characterController.jump();
            }

            if (Input.GetKey(this._sprint))
            {
                isSprinting = true;
            }

            if (Input.GetKey(this._crouch))
            {
                isCrouching = true;
            }

            if (Input.GetKey(this._laid))
            {
                isLaid = true;
            }

            if (Input.GetKey(this._moveForward))
            {
                keyCount++;
            }   

            if (Input.GetKey(this._moveBackward))
            {
                keyCount++;
            }

            if (Input.GetKey(this._moveLeft))
            {
                keyCount++;
            }

            if (Input.GetKey(this._moveRight))
            {
                keyCount++;
            }

            if (keyCount > 0)
            {
                this._characterController.move(keyCount, isSprinting, isCrouching, isLaid);
            }
            
        }

    }

}
