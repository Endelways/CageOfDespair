using UnityEditor;

namespace Core
{
    public interface IInteractableObject
    {
        void Interaction(Hand hand);
    }
}