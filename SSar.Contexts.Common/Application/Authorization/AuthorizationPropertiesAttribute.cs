namespace SSar.Contexts.Common.Application.Authorization
{

    [System.AttributeUsage(
        System.AttributeTargets.Class |
        System.AttributeTargets.Struct)]
    public class AuthorizationProperties : System.Attribute
    {
        public string AuthorizationPropertiesAttribute { get; set; }
    }
}
