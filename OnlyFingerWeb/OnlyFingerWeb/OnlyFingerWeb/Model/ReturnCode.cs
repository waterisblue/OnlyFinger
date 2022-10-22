
namespace OnlyFingerWeb.Entity
{
    public class ReturnCode<T>
    {
        public int code { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }

        public ReturnCode(int code, string message, T? data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
        public ReturnCode()
        {

        }
    }
}
