
namespace OnlyFingerWeb.Entity
{
    public class ReturnCode<T>
    {
        public int code { get; set; }
        public T? message { get; set; }

        public ReturnCode(int code, T? message)
        {
            this.code = code;
            this.message = message;
        }   
        public ReturnCode()
        {

        }
    }
}
