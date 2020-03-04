using UnityEngine;
public class TouchObject : MonoBehaviour
{
    [Range(0, 8)] //  Номер анимации нашего объекта
    public int takeNumber = 0;
    // Номер анимации, который сейчас играет
    internal static int numberAnim = 0;
    //Компонет анимации
    private Animator animator;
    //Комонет занимающиеся отрисовкой
    private MeshRenderer mesh;

    private void Awake()
    {
        // Добавлеям компнент с объекта
        mesh = GetComponent<MeshRenderer>();
        // Добавляем компонент с родительского объекта
        animator = GetComponentInParent<Animator>();
    }


    // Событие происходит когда отпускаеться кнопка мыши при нажатии на наш объект, на телефоне, аналогично простому тачу
    public void OnMouseDown()
    {
        //Присваеваим номеру анимации, номер нашего объекта
        numberAnim = takeNumber;
        // И запускаем анимацию
        animator.SetInteger("Take", numberAnim);
    }
}