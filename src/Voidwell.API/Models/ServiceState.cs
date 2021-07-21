namespace Voidwell.API.Models
{
    public class ServiceState : OriginatorData
    {
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public object Details { get; set; }
    }
}
