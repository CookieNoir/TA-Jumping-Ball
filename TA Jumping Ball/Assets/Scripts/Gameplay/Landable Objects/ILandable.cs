// ��������� ��� ������������ ��������
// ������ �� ������, ���� ������ �� ��������� ����� �� ������ ���, �������������� �������
using UnityEngine;

public interface ILandable
{
    bool TryToLand(in Vector2 landPoint); // ���������� true ��� �������� �����������, false - �����
}