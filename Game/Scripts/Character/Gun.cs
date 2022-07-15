using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum User
{
    player, enemy
}
public class Gun : MonoBehaviour
{
    Camera m_camera;
    public User m_user;
    void Start()
    {
        m_camera = Camera.main;
        Cursor.visible = false;
    }
 
    void Update()
    {
        if(m_user == User.player)
        {
            var lookAtPos = Input.mousePosition;
            lookAtPos.z = transform.position.z - m_camera.transform.position.z;
            lookAtPos = m_camera.ScreenToWorldPoint(lookAtPos);
            transform.up = lookAtPos - transform.position;
        }  
    }
}