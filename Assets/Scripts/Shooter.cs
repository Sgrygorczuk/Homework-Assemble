using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour

{    
    //==================================================================================================================
    // Variables  
    //==================================================================================================================
    
    //The Player Ball that will be spawned 
    [SerializeField] private GameObject preFab;
    //The amount of force that is applied when we shoot the ball 
    [SerializeField] private float forceMultiplier; 
    //The position of where the ball will be shot towards 
    private Transform _pointer;
    //The angel we're currently at
    private float _angel;
    //How much power is put into the shot 
    private float _power;
    //The initial y position of the pointer  
    private float _y;
    //The button used to save the info 
    private Button _button;
    //The text that's on the button 
    private TextMeshProUGUI _textMeshProUGUI;
    //Different States that the Shooter Scripts can be in 
    private enum States
    {
        Wait,
        Angle,
        Power
    }
    //Current State that the script is in 
    private States _currentState = States.Power; 

    //==================================================================================================================
    // Base Functions 
    //==================================================================================================================
    
    //Connects components and saves/sets data 
    private void Start()
    {
        //Get pointer and save its y position 
        _pointer = transform.Find("Pointer");
        _y = _pointer.position.y;
        
        _button = GameObject.Find("Canvas").transform.Find("ActionButton").GetComponent<Button>();
        
        //Connects Text and sets base text 
        _textMeshProUGUI = GameObject.Find("Canvas").transform.Find("ActionButton").transform.Find("Click")
            .GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.text = "Set Power";
    }

    //Goes through the different states and acts upon them 
    private void Update()
    {
        switch (_currentState)
        {
            case States.Wait:
            {
                Waiting();
                break;
            }
            case States.Angle:
            {
                DetermineAngle();
                break;
            }
            case States.Power:
            {
                DeterminePower();
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    //==================================================================================================================
    // State Updates 
    //==================================================================================================================
    
    //Oscillates between -60 and 60 and updates the rotation 
    private void DetermineAngle()
    {
        _angel = 60 * Mathf.Sin(4 * Time.time);
        transform.rotation = Quaternion.Euler(0, 0, _angel);
    }
    
    //Oscillates between -0.5 + _y and +0.5 + _y and updates the position  
    private void DeterminePower()
    {
        _power = 0.5f * Mathf.Sin(4f * Time.time);
        _pointer.position = new Vector3(0, _y + _power, 0);
    }
    //Sets angle and power to be 0 and for it to sit within the spawn circle 
    private void Waiting()
    {
        _power = 0;
        _angel = 0;
        _pointer.position = new Vector3(0, transform.position.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, _angel);
    }

    //==================================================================================================================
    // External Called Methods 
    //==================================================================================================================
    
    //Used by End Goal to give the shooting power back to the player 
    public void ResetButton()
    {
        //Allows for use of button 
        _button.interactable = true;
        //Resets the 
        _pointer.position = new Vector3(0, _y + _power, 0);
        _currentState = States.Power;
    }

    //Used by the button to set in the power chosen by the player 
    public void Action()
    {
        switch (_currentState)
        {
            //First we check the power, once that is selected we set the power to be the distance between the 
            //center of the whole to the point 
            case States.Power:
                _power = Vector2.Distance(transform.position, _pointer.position);
                _currentState = States.Angle;
                _textMeshProUGUI.text = "Set Angle";
                break;
            //Then we select the angle, and once we got it we calculate the x force we will place on the ball and instantiate the ball
            //with the velocity, once ball is sent player can't touch the button till the ball dies 
            case States.Angle:
            {
                _currentState = States.Wait;
                _textMeshProUGUI.text = "Set Power";
                
                //Make ball 
                var instance= Instantiate(preFab, transform.position, Quaternion.identity);
                //Get it's velocity based on power and angle 
                var x = Mathf.Abs(forceMultiplier * Mathf.Abs(_power) * Mathf.Sin(Mathf.Abs((_angel) * Mathf.PI/180)));
                //Make it negative if the angle was negative 
                if (_angel < 0) { x *= -1; }
                //Send it flying 
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(x, 0);
          
                _button.interactable = false;
                break;
            }
            case States.Wait:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    


}
