using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private float _introTime = 3;
    private float _timer;
    // Start is called before the first frame update
    void Awake()
    {
        _timer = _introTime;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        BattleController.Instance.OnPlayerTurn += PlayPlayerTurnAnim;
        BattleController.Instance.OnPlayerMove += PlayPlayerMoveAnim;
        BattleController.Instance.OnEnemyTurn += PlayPlayerTurnAnim;
        BattleController.Instance.OnConclusion += PlayConclusionAnim;
        BattleController.Instance.SetBattleStage(EBattleStage.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;     
        }
    }

    private void CheckEndIntro()
    {
        if (_timer <= 0)
        {
            EndIntro();
            this.enabled = false;
        }
    }

    private void EndIntro()
    {
        BattleController.Instance.SetBattleStage(EBattleStage.PlayerTurn);
    }

    private void PlayPlayerTurnAnim()
    {
        _animator.SetBool("PlayerTurn", true);
    }

    private void PlayPlayerMoveAnim()
    {
        _animator.Play("PlayerMoveCam");
        _animator.SetBool("PlayerTurn", false);
    }

    private void PlayConclusionAnim()
    {
        _animator.Play("ConclusionCam");
    }
}
