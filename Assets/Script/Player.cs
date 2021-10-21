using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody PRb = null;
    private BoxCollider boxCollider = null;

    private Animator PAnima = null; // 하체 애니메이션
    [SerializeField] private GameObject Leg;

    private Animator BodyAnima = null; //상체 애니메이션
    [SerializeField] private Transform Body;


    [SerializeField] private Transform Cam;
    [SerializeField] private GameObject Charactor;

    [SerializeField] private GameObject[] Weapon = null;
    [SerializeField] private bool[] HasWeapon = null;         //현재 소지 무기
    private Weapon equipWeapon = null;                  //장착중인 무기
    private int WeaponIndex = 0;

    private bool isSwap = false;
    private bool isAttack = false;
    private bool SweapDelay = false;
    private bool isReLoding = false; // 총알이 장전중인지 확인



    private bool isRun = false;
    private bool isGround;  //점프가능 확인
    private bool isjump = true;
    private float jumpForce = 7.0f;
    private int hp = 100;
    [SerializeField] float PlayerwalkSpeed = 0f;
    [SerializeField] float PlayerRunSpeed = 0f;



    void Awake()
    {
        GameManager.instance.players.Add(this);

        boxCollider = GetComponent<BoxCollider>();
        PRb = GetComponent<Rigidbody>();
        PAnima = Leg.GetComponentInChildren<Animator>();
        BodyAnima = Body.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        LookAt();
        CharactorMotion();
    }
    private void LookAt()
    {
        //마우스의 이동값 좌표 저장
        Vector2 mouseDelat = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector3 CamAngle = Cam.rotation.eulerAngles; //카메라 앵글을 변수로 저장
        float X = CamAngle.x - mouseDelat.y;

        //카메라 상하의 제한
        if (X < 180f) X = Mathf.Clamp(X, -1f, 35f);
        else X = Mathf.Clamp(X, 310f, 361f);

        //카메라 회전
        Cam.rotation = Quaternion.Euler(X, CamAngle.y + mouseDelat.x, CamAngle.z).normalized;

        //캐릭터 상하 회전
        Body.rotation = Quaternion.Euler(X, CamAngle.y + mouseDelat.x, 0f).normalized;
        //Debug.Log(Body.rotation.x);
    }
    private void CharactorMotion()
    {
        WeaponSwap();
        Move();

        if (WeaponIndex != 0) BodyAnima.SetBool("HasWeapon", true);
        else BodyAnima.SetBool("HasWeapon", false);

        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            Jump();
        }
        if (Input.GetMouseButton(0) && !isSwap)
        {
            Attack();
        }

        if (WeaponIndex > 3 && equipWeapon != null && (Input.GetKeyDown(KeyCode.R) || equipWeapon.currentbullet <= 0) && !isReLoding && equipWeapon.HaveBulletInPocket > 0)
        {
            isReLoding = true;
            Invoke("ReLoadingDelay", 1.0f);
            Debug.Log("장전중!");
            BodyAnima.SetTrigger("ReLoding");
        }
    }
    private void ReLoadingDelay()
    {
        equipWeapon.ReLoad();
        isReLoding = false;
    }

    private void Move()
    {
        //캐릭터의 이동 입력값
        float MoveX = Input.GetAxisRaw("Vertical");
        float MoveZ = Input.GetAxisRaw("Horizontal");
        Vector3 PlayerMove = new Vector3(MoveX, 0f, MoveZ).normalized;

        //캐릭터가 바라볼 방향설정
        Vector3 Lookforward = new Vector3(Cam.forward.x, 0f, Cam.forward.z).normalized;
        Vector3 LookRight = new Vector3(Cam.right.x, 0f, Cam.right.z).normalized;
        Vector3 MoveDir = Lookforward * MoveX + LookRight * MoveZ;

        //플레이어의 forward와 움직임
        Charactor.transform.forward = Lookforward;
        this.transform.position += MoveDir * (isRun ? PlayerRunSpeed : PlayerwalkSpeed) * Time.deltaTime;

        ////////////////////////////////////////////////
        //애니메이션

        PAnima.SetBool("isWalk", PlayerMove.magnitude != 0);
        PAnima.SetBool("isRun", Input.GetKey(KeyCode.LeftShift));
    }
    private void Jump()
    {
        PRb.velocity = transform.up * jumpForce;
        PAnima.SetTrigger("DoJump");
        if (!isGround) PAnima.SetBool("isJump", isjump);
        else PAnima.SetBool("isJump", !isjump);
    }
    private bool IsGround()
    {
        //RayCast를 이용하여 플레이어의 아래쪽에 닿는 것이 있는지 확인.
        isGround = Physics.Raycast(transform.position, Vector3.down, boxCollider.bounds.extents.y + 0.1f);
        return isGround;
    }
    private void WeaponSwap()
    {
        if (!SweapDelay)
        {
            if (Input.GetKeyDown(KeyCode.Q)) WeaponIndex--;
            if (Input.GetKeyDown(KeyCode.E)) WeaponIndex++;
            if (WeaponIndex > 9) WeaponIndex = 0;
            else if (WeaponIndex < 0) WeaponIndex = 9;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);
                while (HasWeapon[WeaponIndex] == false)
                {
                    WeaponIndex++;
                    if (WeaponIndex > 9) WeaponIndex = 0;
                }
                WeaponSwapMotion();
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);
                while (HasWeapon[WeaponIndex] == false)
                {
                    WeaponIndex--;
                    if (WeaponIndex < 0) WeaponIndex = 9;
                }
                WeaponSwapMotion();
            }
        }
    }

    private void WeaponSwapMotion()
    {
        BodyAnima.SetTrigger("isSwap");
        isSwap = true;
        Invoke("SwapOut", 0.5f);
        equipWeapon = Weapon[WeaponIndex].GetComponent<Weapon>();
        Invoke("SpawnWeapon", 0.5f);
    }

    private void SpawnWeapon()
    {
        equipWeapon.gameObject.SetActive(true);
    }

    private void SwapOut()
    {
        isSwap = false;
    }

    private void Attack()
    {

        if (equipWeapon.type == global::Weapon.Type.Hand && !isAttack) { }

        else if (equipWeapon.type == global::Weapon.Type.SwingWeapon && !isAttack&&!isReLoding)
        {
            isAttack = true;
            BodyAnima.SetTrigger("DoSwing");
            StartCoroutine("IsAttack");
        }
        else if (equipWeapon.type == global::Weapon.Type.Electric && !isAttack && !isReLoding)
        {
            isAttack = true;
            BodyAnima.SetTrigger("DoSawing");
            StartCoroutine("IsAttack");
        }
        else if (equipWeapon.type == global::Weapon.Type.Gun && !isAttack && !isReLoding)
        {
            isAttack = true;
            BodyAnima.SetTrigger("DoShoot");
            StartCoroutine("IsAttack");
        }
        else if (equipWeapon.type == (global::Weapon.Type.Rebound) && !isAttack && !isReLoding)
        {
            isAttack = true;
            BodyAnima.SetTrigger("DoRebound");
            StartCoroutine("IsAttack");
        }
    }
    IEnumerator IsAttack()
    {
        equipWeapon.Attack();
        yield return new WaitForSeconds(equipWeapon.AtkDelay);
        isAttack = false;
    }

    public void BuyWeapon(int Num)
    {
        HasWeapon[Num] = true;
    }


    //데미지 받기
    public void Damage(int _dmg)
    {
        hp -= _dmg;
        Debug.Log(hp);
        if (hp <= 0) Destroy(gameObject);
    }
    IEnumerator poison()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            //foreach (MeshRenderer mesh in meshs)
            //{
            //    mesh.material.color = Color.green;
            //}
            Damage(3);
            //yield return new WaitForSeconds(0.25f);
            //foreach (MeshRenderer mesh in meshs)
            //{
            //    mesh.material.color = Color.white;
            //}
        }
    }



}
