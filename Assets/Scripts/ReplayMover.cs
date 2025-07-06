using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
            //todo comment: зачем нужны эти проверки?
            // Проверяем наличие компонента и записей
            if(!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
				//todo comment: Для чего выключается этот компонент?

				enabled = false;
			}

			_index = 0;
			_prev = _save.Records[0];


		}

		private void Update()
		{
            if(_index >= _save.Records.Count)
            {
                enabled = false;
                Debug.Log($"<b>{name}</b> finished", this);
                return;
            }

            var curr = _save.Records[_index];
			//todo comment: Что проверяет это условие (с какой целью)?
			//  сравнение прошедшего времени с текущим для выполнения условий
			if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
				//todo comment: Для чего нужна эта проверка?
				// проверка количества обработанных записей
				//if (_index >= _save.Records.Count)
				//{
				//	enabled = false;
				//	Debug.Log($"<b>{name}</b> finished", this);
				//}
			}
			//todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
			// разница во времени
			var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
            //todo comment: Зачем нужна эта проверка?
            // проверка является ли значение переменной delta не числом и присваивает начальное значение
            if(float.IsNaN(delta)) delta = 0f;
            //todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
            // Вычисляем интерполяцию между точками
            // Плавное перемещение между точками
            transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}

}