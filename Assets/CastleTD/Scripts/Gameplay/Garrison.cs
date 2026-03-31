using System.Collections.Generic;
using UnityEngine;

public class Garrison : MonoBehaviour
{
    private List<Unit> _activeUnits;

    public IReadOnlyList<Unit> ActiveUnits => _activeUnits;

    public void AddUnit(Unit unit)
    {
        _activeUnits.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _activeUnits.Remove(unit);
    }
}