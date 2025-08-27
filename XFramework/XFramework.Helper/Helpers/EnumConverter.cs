namespace XFramework.Helper.Helpers
{
    public static class EnumConverter
    {
        public static TEnum CharToEnum<TEnum>(string value) where TEnum : struct, Enum
        {
            foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
            {
                var shortName = string.Concat(enumValue.ToString().Where(char.IsUpper));
                if (shortName.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return (TEnum)enumValue;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(value), $"'{value}' için {typeof(TEnum).Name} bulunamadı.");
        }

        public static string EnumToChar<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return string.Concat(value.ToString().Where(char.IsUpper));
        }

    }
}
