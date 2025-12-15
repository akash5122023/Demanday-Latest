namespace Serenity
{
    public static class Check
    {
        public static T NotNull<T>(T value, string name)
        {
            if (value == null)
                throw new System.ArgumentNullException(name);
            return value;
        }

        public static string NotNullOrWhiteSpace(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new System.ArgumentNullException(name);
            return value;
        }
    }
}
