using UnityEngine;

public class LineRender : MonoBehaviour
{
    //==================================================================================================================
    // Variables  
    //==================================================================================================================
    
    //The line that we will draw
    private LineRenderer _lineRenderer;
    //The points between the line 
    [SerializeField] private Transform[] points;
   
    //==================================================================================================================
    // Base Methods 
    //==================================================================================================================
    
    //Connects the components and tells it how many points it has to keep track of 
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = points.Length;
    }

    //Updates the position of the positions of the points to the line 
    private void Update()
    {
        for (var i = 0; i < points.Length; i++)
        {
            _lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
