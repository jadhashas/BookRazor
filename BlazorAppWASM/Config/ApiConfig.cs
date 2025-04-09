namespace BlazorAppWASM.Config
{
    public static class ApiConfig
    {
        public const string BaseUrl = "http://localhost:5023/";
        public const string AuthEndpoint = "api/auth";
        
        public static class Auth
        {
            public static string Login => $"{AuthEndpoint}/login";
            public static string Register => $"{AuthEndpoint}/register";
            public static string Me => $"{AuthEndpoint}/me";
        }
    }
} 