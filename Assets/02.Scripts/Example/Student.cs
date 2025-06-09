using UnityEngine;

public class Student : MonoBehaviour
{
    public string Name;
    public int Age;
    public string Gender;

    public override string ToString()
    {
        return $"{Name}{Age}{Gender}";
    }
}
