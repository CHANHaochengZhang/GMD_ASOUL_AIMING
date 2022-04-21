using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Weapon swing
public class WeaponSway : MonoBehaviour
{
    public float amount;//幅度
    public float smooth;//平滑值
    public float maxAmount;//最大幅度摇摆

    [SerializeField] private Vector3 originalPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X")*amount;
        float moveY = -Input.GetAxis("Mouse Y")*amount;
        Mathf.Clamp(moveX, -maxAmount, maxAmount);
        Mathf.Clamp(moveY, -maxAmount, maxAmount);
        

        Vector3 finallyPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition,finallyPosition+originalPosition,Time.deltaTime * smooth);
    }
}
