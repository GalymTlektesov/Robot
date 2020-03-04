using UnityEngine;

namespace Lean.Touch
{
	/// <summary>Этот script позволяет масштабировать текущий GameObject.</summary>
	[HelpURL(LeanTouch.HelpUrlPrefix + "LeanScale")]
	public class LeanScale : MonoBehaviour
	{
		[Tooltip("Игнорировать пальцы с StartedOverGui?")]
		public bool IgnoreStartedOverGui = true;

		[Tooltip("Игнорировать пальцы с IsOverGui?")]
		public bool IgnoreIsOverGui;

		[Tooltip("Позволяет принудительно вращать определенное количество пальцев (0 = любой)")]
		public int RequiredFingerCount;

		[Tooltip("Требуется ли для масштабирования объект, который будет выбран?")]
		public LeanSelectable RequiredSelectable;

		[Tooltip("Камера, которая будет использоваться для расчета увеличения (Нет = MainCamera)")]
		public Camera Camera;

		[Tooltip("Если вы хотите, чтобы колесо мыши имитировало зажатие, установите его здесь")]
		[Range(-1.0f, 1.0f)]
		public float WheelSensitivity;

		[Tooltip("Должно ли масштабирование быть производительным относительно центра пальца?")]
		public bool Relative;

		[Tooltip("Должно ли значение шкалы быть зафиксировано?")]
		public bool ScaleClamp;

		[Tooltip("Минимальное значение шкалы по всем осям")]
		public Vector3 ScaleMin;

		[Tooltip("Максимальное значение шкалы по всем осям")]
		public Vector3 ScaleMax;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Start();
		}
#endif

		protected virtual void Start()
		{
			if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}

		protected virtual void Update()
		{
			// Получить пальцы, которые мы хотим использовать
			var fingers = LeanSelectable.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount, RequiredSelectable);

			// Рассчитайтываем масштаб щепотки и убедитесь, что он действителен
			var pinchScale = LeanGesture.GetPinchScale(fingers, WheelSensitivity);

			if (pinchScale != 1.0f)
			{
				// Выполните перевод, если это относительная шкала
				if (Relative == true)
				{
					var pinchScreenCenter = LeanGesture.GetScreenCenter(fingers);

					if (transform is RectTransform)
					{
						TranslateUI(pinchScale, pinchScreenCenter);
					}
					else
					{
						Translate(pinchScale, pinchScreenCenter);
					}
				}

				// Выполнить масштабирование
				Scale(transform.localScale * pinchScale);
			}
		}

		protected virtual void TranslateUI(float pinchScale, Vector2 pinchScreenCenter)
		{
			// Положение экрана трансформации
			var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position);

			// Отодвиньте положение экрана от контрольной точки на основе scale
			screenPoint.x = pinchScreenCenter.x + (screenPoint.x - pinchScreenCenter.x) * pinchScale;
			screenPoint.y = pinchScreenCenter.y + (screenPoint.y - pinchScreenCenter.y) * pinchScale;

			// Конвертировать обратно в мировое пространство
			var worldPoint = default(Vector3);

			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true)
			{
				transform.position = worldPoint;
			}
		}

		protected virtual void Translate(float pinchScale, Vector2 screenCenter)
		{
			// Убедитесь, что camera существует
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
				// Положение экрана transform
				var screenPosition = camera.WorldToScreenPoint(transform.position);

				// Отодвиньте положение экрана от контрольной точки на основе scale
				screenPosition.x = screenCenter.x + (screenPosition.x - screenCenter.x) * pinchScale;
				screenPosition.y = screenCenter.y + (screenPosition.y - screenCenter.y) * pinchScale;

				// Конвертировать обратно в мировое пространство
				transform.position = camera.ScreenToWorldPoint(screenPosition);
			}
			else
			{
				Debug.LogError("Failed to find camera. Either tag your cameras MainCamera, or set one in this component.", this);
			}
		}

		protected virtual void Scale(Vector3 scale)
		{
			if (ScaleClamp == true)
			{
				scale.x = Mathf.Clamp(scale.x, ScaleMin.x, ScaleMax.x);
				scale.y = Mathf.Clamp(scale.y, ScaleMin.y, ScaleMax.y);
				scale.z = Mathf.Clamp(scale.z, ScaleMin.z, ScaleMax.z);
			}

			transform.localScale = scale;
		}
	}
}