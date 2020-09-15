using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_cow;
    [SerializeField]
    private AudioSource m_sheep;

    public void PlayAnimalSound(EAnimalType type)
    {
        switch (type)
        {
            case EAnimalType.COW : m_cow.Play(); break;
            case EAnimalType.SHEEP : m_sheep.Play(); break;

            default : break;
        }
    }
}
