// Интерфейс для приземляемых объектов
// Сделан на случай, если падать на платформы будет не только шар, контролируемый игроком
using UnityEngine;

public interface ILandable
{
    bool TryToLand(in Vector2 landPoint); // Возвращает true при успешном приземлении, false - иначе
}