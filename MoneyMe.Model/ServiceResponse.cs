namespace MoneyMe.Model
{
    public sealed class ServiceResponse
    {
        public string DateTime { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public dynamic? Output { get; set; } = null;
        public bool HasError { get; set; } = false;
    }
}
