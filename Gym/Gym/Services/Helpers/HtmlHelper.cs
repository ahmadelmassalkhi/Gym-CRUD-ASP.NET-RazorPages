namespace Gym.Services.Helpers
{
    public static class HtmlHelper
    {
        // Static method to wrap text in a green span
        public static string WrapInGreenSpan(string text)
        {
            return $"<span class='text-green'>{text}</span>";
        }

        // Static method to wrap text in a red span
        public static string WrapInRedSpan(string text)
        {
            return $"<span class='text-red'>{text}</span>";
        }


        // Static method to highlight text
        public static string HighlightText(string text)
        {
            return $"<span class='highlight'>{text}</span>";
        }
    }
}
