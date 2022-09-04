using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Game _game;
    [SerializeField] private KeyCode _alternativeInputKey;

    private void Update()
    {
        if (_game.Controllable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)
        || Input.GetKeyDown(_alternativeInputKey)
#if UNITY_ANDROID
            || (Input.touchCount > 0
        && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
            )
            {
                _ball.TryToJump();
            }
        }
    }
}