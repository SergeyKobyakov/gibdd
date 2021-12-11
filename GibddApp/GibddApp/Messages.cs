namespace GibddApp
{
    internal static class Messages
    {
        public static void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
