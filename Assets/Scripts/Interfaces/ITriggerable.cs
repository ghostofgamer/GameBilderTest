using UnityEngine;

public interface ITriggerable
{
    public void OnTriggerEnter(Collider other);
    public void OnTriggerExit(Collider other);
}
