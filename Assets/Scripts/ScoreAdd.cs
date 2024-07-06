using TMPro;
using UnityEngine;

public class ScoreAdd : MonoBehaviour
{
    //==================================================================================================================
    // Variables  
    //==================================================================================================================

    [SerializeField] private TextMeshPro textMeshPro;
    private float _x;

    //==================================================================================================================
    // Base Methods 
    //==================================================================================================================

    //Saves the original X position and starts the timer till the object is destroyed    
    private void Start()
    {
        _x = transform.position.x;
        Invoke($"Death", 3.1f);
    }
    
    //Oscillates around the x axis while moving up on the y axis  
    private void Update()
    {
        var position = transform.position;
        position = new Vector3(_x + Mathf.Sin(4f * Time.time), position.y, 0.0f);
        position += Vector3.up * Time.deltaTime;
        transform.position = position;
    }

    //==================================================================================================================
    // Set Up and Death Methods 
    //==================================================================================================================

    //Used by End Goals to set the value that will be shown in the text
    public void SetValue(int i)
    {
        textMeshPro.text = "+" + i;
    }

    //Destroys the object after Invoked by Start Method  
    private void Death()
    {
        Destroy(gameObject);
    }
    
}
