using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10.0f; // Velocidad de movimiento de la cámara
    public float lookSpeed = 2.0f;  // Velocidad de rotación de la cámara
    public float verticalSpeed = 5.0f; // Velocidad de movimiento vertical (subir/bajar)

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento de la cámara con WSAD
        float horizontal = Input.GetAxis("Horizontal"); // A y D
        float vertical = Input.GetAxis("Vertical");     // W y S

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = transform.TransformDirection(direction);
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Movimiento vertical con Espacio (subir) y Shift (bajar)
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime, Space.World);
        }

        // Rotación de la cámara con el ratón
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90, 90); // Limitar la rotación vertical

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Ocultar y bloquear el cursor en el centro de la pantalla al volver al juego
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
