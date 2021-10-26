using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider hpbar;
    [SerializeField] private Text hptext;
    [SerializeField] private Text Cointext;
    private Rigidbody PRb = null;
    private BoxCollider boxCollider = null;

    private Animator PAnima = null; // ��ü �ִϸ��̼�
    [SerializeField] private GameObject Leg;

    private Animator BodyAnima = null; //��ü �ִϸ��̼�
    [SerializeField] private Transform Body;


    [SerializeField] private Transform Cam;
    [SerializeField] private GameObject Charactor;

    [SerializeField] private GameObject[] Weapon = null;
    [SerializeField] private bool[] HasWeapon = null;         //���� ���� ����
    private Weapon equipWeapon = null;                  //�������� ����
    private int WeaponIndex = 0;

    private bool isSwap = false;
    private bool isAttack = false;
    private bool SweapDelay = false;
    private bool isReLoding = false; // �Ѿ��� ���������� Ȯ��

    private bool isRun = false;
    private bool isGround;  //�������� Ȯ��
    private bool isjump = true;
    private float jumpForce = 3.0f;
    private float hp = 10000f;
    private float curhp = 100f;
    private float PlayerwalkSpeed = 6f;
    private float PlayerRunSpeed = 12f;

    public int PlayerCoin = 55000;

    void Awake()
    {
        GameManager.instance.players.Add(this);

        boxCollider = GetComponent<BoxCollider>();
        PRb = GetComponent<Rigidbody>();
        PAnima = Leg.GetComponentInChildren<Animator>();
        BodyAnima = Body.GetComponent<Animator>();
        hpbar.value = curhp / hp;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerCoin);
        
        Hpbar();

        LookAt();
        CharactorMotion();
        hptext.text = hp.ToString();
        Cointext.text = PlayerCoin.ToString();
    }
    private void LookAt()
    {
        //���콺�� �̵��� ��ǥ ����
        Vector2 mouseDelat = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector3 CamAngle = Cam.rotation.eulerAngles; //ī�޶� �ޱ��� ������ ����
        float X = CamAngle.x - mouseDelat.y;

        //ī�޶� ������ ����
        if (X < 180f) X = Mathf.Clamp(X, -1f, 35f);
        else X = Mathf.Clamp(X, 310f, 361f);

        //ī�޶� ȸ��
        Cam.rotation = Quaternion.Euler(X, CamAngle.y + mouseDelat.x, CamAngle.z).normalized;

        //ĳ���� ���� ȸ��
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
            Debug.Log("������!");
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
        //ĳ������ �̵� �Է°�
        float MoveX = Input.GetAxisRaw("Vertical");
        float MoveZ = Input.GetAxisRaw("Horizontal");
        Vector3 PlayerMove = new Vector3(MoveX, 0f, MoveZ).normalized;

        //ĳ���Ͱ� �ٶ� ���⼳��
        Vector3 Lookforward = new Vector3(Cam.forward.x, 0f, Cam.forward.z).normalized;
        Vector3 LookRight = new Vector3(Cam.right.x, 0f, Cam.right.z).normalized;
        Vector3 MoveDir = Lookforward * MoveX + LookRight * MoveZ;

        //�÷��̾��� forward�� ������
        Charactor.transform.forward = Lookforward;
        this.transform.position += MoveDir * (isRun ? PlayerRunSpeed : PlayerwalkSpeed) * Time.deltaTime;

        ////////////////////////////////////////////////
        //�ִϸ��̼�

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

        else if (equipWeapon.type == global::Weapon.Type.SwingWeapon && !isAttack && !isReLoding)
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


    //������ �ޱ�
    public void Damage(int _dmg)
    {
        hp -= _dmg;
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


    private void Hpbar()
    {
        hpbar.value = hp / curhp;
    }

    public void HpUp()
    {
        hp += 10;
    }

    public void SpeedUp()
    {
        PlayerwalkSpeed+=0.6f;
        PlayerRunSpeed+=1.2f;
    }

    public void JumpForceUp()
    {
        jumpForce += 0.3f;
    }

    public int SetCoin()
    {
        return PlayerCoin;
    }

    public void PlayerCoinMinus(int value)
    {
        PlayerCoin -= value;
    }

    
}
