namespace Service.Application.Gateway;

public static class Topics
{
    public const string ResponseTopic = "notifcation-response";
    
    
    private const string SmsTopic = "notifcation-sms";
    private const string EmailTopic = "notifcation-email";
    private const string PushTopic = "notifcation-push";

    public static string GetTopicForType(string type)
    {
        return type switch
        {
            "sms" => SmsTopic,
            "email" => EmailTopic,
            "push" => PushTopic,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    
}