using UnityEngine;
using System.Collections;

public class TableTile : BaseTile
{
    [SerializeField] private EggType _desiredType;
    private bool _isHappy = false;
    private bool _isMad = false;
    private SpriteRenderer _renderer;
    private Animator _anim;

    public override void Start()
    {
        base.Start();
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        CreateNewOrder();
    }

    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);

        if(_isHappy || _isMad)
        {
            Destroy(egg.gameObject);
            return;
        }

        if(egg.GetEggType() != _desiredType)
        {
            _isMad = true;
            Destroy(egg.gameObject);
            StartCoroutine(GetMad());
            return;
        }

        _isHappy = true;
        Destroy(egg.gameObject);
        StartCoroutine(WaitForNewOrder());
    }

    // Chooses a new order
    public void CreateNewOrder()
    {
        _desiredType = (EggType)Random.Range(0, 4);
    }

    // Waits for 0.5 - 5 seconds then creates a new order
    private IEnumerator WaitForNewOrder()
    {
        _anim.SetBool("isHappy", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("isHappy", false);
        _isHappy = false;

        CreateNewOrder();
    }

    private IEnumerator GetMad()
    {
        _anim.SetBool("isMad", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("isMad", false);

        _isMad = false;
    }
}
