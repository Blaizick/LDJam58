
namespace Blaze
{
    public static class TextFormatter
    {
        public const string thousand = "K.";
        public const string million = "M.";

        public static string Format(float count)
        {
            float thousands = count / 1000;
            float millions = count / 1000000;

            if (millions >= 1f)
            {
                return $"{millions} {million}";
            }
            if (thousands >= 1f)
            {
                return $"{thousands} {thousand}";
            }

            return count.ToString();
        }
    }
}