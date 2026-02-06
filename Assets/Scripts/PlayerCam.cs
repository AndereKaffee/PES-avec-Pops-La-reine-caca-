using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystems.Camera
{
    public class PlayerCam : MonoBehaviour
    {
        public new Transform camera;
        public Transform camRotation;
        public Transform camHolder;
        public Transform player;

        private Transform cameraPosition;

        float _xRotation;
        float _yRotation;

        void Start()
        {
            cameraPosition = camera.transform;
        }

        void Update()
        {
            float mouseY = Mouse.current.delta.ReadValue().y * 0.01f * 30 * 3 * Time.timeScale;
            float mouseX = Mouse.current.delta.ReadValue().x * 0.01f * 30 * 3 * Time.timeScale;

            _yRotation += mouseX;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            camRotation.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            camHolder.position = player.position;
        }
    }
}
