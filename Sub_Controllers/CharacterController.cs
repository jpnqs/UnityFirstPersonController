using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPersonController
{


    public class CharacterController
    {

        private GameObject _controller;
        private Rigidbody _rigidbody;
        private GameObject _camera;
        private float _speed;
        private float _sprintMultiplier;
        private float _crouchMultiplier;
        private float _laidMultiplier;

        public enum RotationAxis
        {
            MouseX = 1,
            MouseY = 2
        }

        public RotationAxis axes = RotationAxis.MouseX;
        private float _sensVertical = 2.0f;
        private float _sensHorizontal = 2.0f;
        private float _minVert = -80.0f;
        private float _maxVert = 80.0f;

        public float _rotationX = 0;

        public void cameraMovementX ()
        {
            _controller.transform.Rotate(0, Input.GetAxis("Mouse X") * _sensHorizontal, 0);
        }

        public void cameraMovementY ()
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensVertical;
            _rotationX = Mathf.Clamp(_rotationX, _minVert, _maxVert);
            float rotationY = _camera.transform.localEulerAngles.y;
            Debug.Log(_rotationX);
            _camera.transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controller"></param>
        public CharacterController (GameObject controller, GameObject camera, float movementSpeed, float sprintMultiplier, float crouchMultiplier, float laidMultiplier)
        {
            _camera = camera;
            lockCursor();
            _controller = controller;
            _rigidbody = _controller.GetComponent<Rigidbody>();
            _speed = movementSpeed;
            _sprintMultiplier = sprintMultiplier;
            _crouchMultiplier = crouchMultiplier;
            _laidMultiplier = laidMultiplier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCount"></param>
        /// <param name="sprinting"></param>
        /// <param name="crouching"></param>
        /// <param name="laid"></param>
        public void move (int keyCount, bool sprinting, bool crouching, bool laid)
        {

            float speed = _speed / keyCount;

            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            float movementModifier = 1;
            if (sprinting)
                movementModifier *= _sprintMultiplier;
            else if (crouching)
                movementModifier *= _crouchMultiplier;
            else if (laid)
                movementModifier *= _laidMultiplier;

            _controller.transform.Translate( horizontalAxis * Time.deltaTime * speed * movementModifier,
                                             0f, 
                                             verticalAxis * Time.deltaTime * speed * movementModifier );

        }

        public void jump ()
        {
            _rigidbody.AddForce(_controller.transform.up * 200);
        }

        public void lockCursor ()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void unlockCursor ()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

}