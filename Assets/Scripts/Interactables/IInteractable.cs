
using UnityEngine;

public interface IInteractable
{
    public abstract void OnEnterRange(); 
    public abstract void OnExitRange();
    public abstract void OnInteract(Transform interactor); 
}
