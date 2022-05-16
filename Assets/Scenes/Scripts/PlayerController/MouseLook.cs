using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Camera rotate by player mouse moving
 */
public class MouseLook : MonoBehaviour
{
    public float recoilRate;
    public WeaponController weaponController;
    
    public float mouseSensitivity = 1f;
    public Transform playerBody; // player position

    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // recoilRate = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Mouse X")*mouseSensitivity;
        float y = Input.GetAxisRaw("Mouse Y")*mouseSensitivity;
        // Debug.Log("x "+x);
        xRotation -= y;
        Recoil();
        xRotation= Mathf.Clamp(xRotation, -90f, 80f);
        playerBody.Rotate(Vector3.up*x);
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
    }

    private void Recoil()
    {
        if (Input.GetMouseButton(0)&& weaponController.currentBullets>1 && weaponController.currentBullets<weaponController.bulletsMag-3)
        {
            xRotation = xRotation-recoilRate*Time.deltaTime;
        }
    }
}
