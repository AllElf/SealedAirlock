using UnityEngine;

public class InstructionButtonScript : MonoBehaviour
{
    [SerializeField] bool _enabled = false;
    [SerializeField] Animator _animator;
    [SerializeField] RectTransform _rectTransform;


    private void OnMouseDown()
    {
        _enabled = !_enabled;
        if(_animator != null)
        {
            Instruction();
        }
        
    }
    void Instruction()
    {
        if (_enabled == true)
        {
            _animator.Play("Instruction manual Scale ON");
        }
        else if (_enabled == false) 
        {
            _animator.Play("Instruction manual Scale");
        }
    }
}
