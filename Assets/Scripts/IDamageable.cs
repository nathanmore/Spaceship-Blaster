using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implement in objects that have health and can be damaged
public interface IDamageable<T>
{
    void Damage(T damage);
}
