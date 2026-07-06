using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    float stamina = 5f;
    bool isGrounded = true;
    private int health;
    public enum Weapons
    {
        None,
        Pistol,
        Rifle,
        MiniGun
    }

    [SerializeField] Animator anim;
    [SerializeField] GameObject pistol, rifle, miniGun;
    bool isPistol, isRifle, isMiniGun;
    [SerializeField] Image pistolUI, rifleUI, miniGunUI, cusror;
    // Referencja do AudioSource
    [SerializeField] AudioSource characterSounds;
    // Referencja do klipu audio skoku
    [SerializeField] AudioClip jump;
    void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (direction.x != 0f || direction.z != 0f)
        {
            // Je¿eli AudioSource nie odtwarza ¿adnych dŸwiêków i jesteœmy na ziemi, to...
            if (!characterSounds.isPlaying && isGrounded)
            {
                // Odtwarzanie dŸwiêku
                characterSounds.Play();
            }
            anim.SetBool("Run", true);
        }
        if (direction.x == 0f && direction.z == 0f)
        {
            // Wy³¹czanie dŸwiêku, gdy postaæ siê zatrzymuje
            characterSounds.Stop();
            anim.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {

            stamina -= Time.deltaTime;
            currentSpeed = shiftSpeed;

            if (stamina > 5f)
            {
                stamina = 5f;
            }
            else if (stamina < 0)
            {
                stamina = 0;
            }

        }
        else
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Wy³¹czanie dŸwiêku biegu
            characterSounds.Stop();
            // Tworzenie tymczasowego Ÿród³a audio dla skoku
            AudioSource.PlayClipAtPoint(jump, transform.position);
            anim.SetBool("Jump", true);
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && isPistol)
        {
            ChooseWeapon(Weapons.Pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isRifle)
        {
            ChooseWeapon(Weapons.Rifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && isMiniGun)
        {
            ChooseWeapon(Weapons.MiniGun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChooseWeapon(Weapons.None);
        }

        //Tutaj dopisz logikê dla miniguna i dla braku broni
    }
    public void ChangeHealth(int count)
    {
        // odejmowanie zdrowia
        health -= count;
        // jeœli zdrowie spadnie do zera lub ni¿ej, to...
        if (health <= 0)
        {
            // Aktywowanie animacji œmierci
            anim.SetBool("Die", true);
            // Usuniêcie broni
            ChooseWeapon(Weapons.None);
            // Wy³¹czenie skryptu PlayerController, co uniemo¿liwia ruch gracza
            this.enabled = false;
            //Przetestujemy to wkrótce, jak tylko zaimplementujemy przeciwników! //coœ siê stanie
        }
    }
        void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }

    public void ChooseWeapon(Weapons weapons)
    {
        anim.SetBool("Pistol", weapons == Weapons.Pistol);
        anim.SetBool("Assault", weapons == Weapons.Rifle);
        anim.SetBool("MiniGun", weapons == Weapons.MiniGun);
        anim.SetBool("NoWeapon", weapons == Weapons.None);
        pistol.SetActive(weapons == Weapons.Pistol);
        rifle.SetActive(weapons == Weapons.Rifle);
        miniGun.SetActive(weapons == Weapons.MiniGun);
        if (weapons != Weapons.None)
        {
            cusror.enabled = true;
        }
        else
        {
            cusror.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "pistol":
                if (!isPistol)
                {
                    isPistol = true;
                    pistolUI.color = Color.white;
                    ChooseWeapon(Weapons.Pistol);
                }
                break;
            case "rifle":
                if (!isRifle)
                {
                    isRifle = true;
                    rifleUI.color = Color.white;
                    ChooseWeapon(Weapons.Rifle);
                }
                break;
            case "minigun":
                if (!isMiniGun)
                {
                    isMiniGun = true;
                    miniGunUI.color = Color.white;
                    ChooseWeapon(Weapons.MiniGun);
                }
                break;
            // Tu napisz logikê dla miniguna
            default:
                break;
        }
        Destroy(other.gameObject);
    }
}
