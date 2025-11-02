
namespace SGC.Abstracciones.Modelos
{
    public class CustomResponse<T>
    {
        public bool EsError { get; set; }

        public string Mensaje { get; set; }

        public T Data { get; set; }

        public CustomResponse()
        {
            EsError = false;
            Mensaje = "Acción realizada con éxito.";
        }
    }
}
