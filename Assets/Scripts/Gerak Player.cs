using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GerakPemain : MonoBehaviour
{
    public float kecepatanJalan = 4.5f;
    public float kecepatanLari = 7.5f;
    public float gravitasi = -9.81f;
    public float sensitivitasMouse = 2.5f;
    public Transform kameraPemain;

    private CharacterController controller;
    private Vector3 kecepatanJatuh;
    private float rotasiX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (kameraPemain == null && Camera.main != null)
        {
            kameraPemain = Camera.main.transform;
        }
    }

    void Update()
    {
        if (controller.isGrounded && kecepatanJatuh.y < 0f)
        {
            kecepatanJatuh.y = -2f;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        float kecepatanAktif = Input.GetKey(KeyCode.LeftShift) ? kecepatanLari : kecepatanJalan;

        Vector3 arahGerak = transform.right * inputX + transform.forward * inputZ;
        controller.Move(arahGerak * kecepatanAktif * Time.deltaTime);

        kecepatanJatuh.y += gravitasi * Time.deltaTime;
        controller.Move(kecepatanJatuh * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivitasMouse;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivitasMouse;

            transform.Rotate(Vector3.up * mouseX);

            rotasiX -= mouseY;
            rotasiX = Mathf.Clamp(rotasiX, -80f, 80f);

            if (kameraPemain != null)
            {
                kameraPemain.localRotation = Quaternion.Euler(rotasiX, 0f, 0f);
            }
        }
    }
}