namespace RollerStore.Core.Exeptions
{
    public class ValidationException : System.Exception
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
}
