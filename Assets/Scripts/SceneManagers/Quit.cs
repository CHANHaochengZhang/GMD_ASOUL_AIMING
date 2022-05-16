using UnityEngine;
using System.Collections;
 
public class Quit : MonoBehaviour
{
    // public
    public int windowWidth = 400;
    public int windowHight = 150;
    public float fadeSpeed = 3f;
 
    // private
    Rect windowRect ;
    int windowSwitch = 0;
    float alpha = 0;
 
    void GUIAlphaColor_0_To_1 ()
    {
        if (alpha < 1) {
            alpha += Time.deltaTime;
            GUI.color = new Color (1, 1, 1, alpha);
        } 
    }
    
    /*
    private void FadeToClear()
    {
        tex.color = Color.Lerp(tex.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    */
 
    // Init
    void Awake ()
    {
        windowRect = new Rect (
            (Screen.width - windowWidth) / 2,
            (Screen.height - windowHight) / 2,
            windowWidth,
            windowHight);
    }
 
    void Update ()
    {        
    }
 
    void OnGUI ()
    {  
        GUILayout.BeginHorizontal();
 
        if (GUILayout.Button ("Quit", GUILayout.Width (70), GUILayout.Height (30)) || Input.GetKeyDown ("escape")) {
            windowSwitch = 1;
            alpha = 0; // Init Window Alpha Color
        }
 
        GUILayout.Space (30);
 
        if (GUILayout.Button ("Reset", GUILayout.Width (70), GUILayout.Height (30))) {
            Application.LoadLevel (0);
        }
 
        GUILayout.EndHorizontal();
 
 
        if (windowSwitch == 1) {
            GUIAlphaColor_0_To_1 ();
            windowRect = GUI.Window (0, windowRect, QuitWindow, "Quit Window");
        }
    }
 
    void QuitWindow (int windowID)
    {
        GUI.Label (new Rect (100, 50, 300, 30), "Are you sure you want to quit game ?");
 
        if (GUI.Button (new Rect (80, 110, 100, 30), "Quit")) {
            Application.Quit ();
        } 
        if (GUI.Button (new Rect (220, 110, 100, 30), "Cancel")) {
            windowSwitch = 0; 
        } 
 
        GUI.DragWindow (); 
    }
 
}