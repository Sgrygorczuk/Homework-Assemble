using UnityEngine;

public class BouncerHit : MonoBehaviour
{
    //==================================================================================================================
    // Variables  
    //==================================================================================================================
    //The pre fab text that will spawn 
    [SerializeField] private GameObject preFab;
    //Animator that will make the bounce animate 
    private  Animator _animator;
    //Value of hitting a bouncer 
    private const int Score = 5;
    //Used to pass the value to the game flow script 
    private GameFlow _gameFlow;
    //Plays SFX when the player hits the bouncer 
    private AudioSource _audioSource;

    //==================================================================================================================
    // Variables  
    //==================================================================================================================
    
    //Connects components 
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _gameFlow = GameObject.Find("GameFlow").GetComponent<GameFlow>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    //==================================================================================================================
    // On Collision Method   
    //==================================================================================================================
    private void OnCollisionEnter2D(Collision2D col)
    {
        //Start the hit animation 
        _animator.Play($"BallHit");
        //Passes in the score 
        _gameFlow.ScorePoint(Score);
        //Creates the text and passes the number to it
        var score = Instantiate(preFab, transform.position, Quaternion.identity);
        score.GetComponent<ScoreAdd>().SetValue(Score);
        //Plays the SFX 
        _audioSource.Play();
    }
}
