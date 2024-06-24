using System.Windows.Forms;

namespace Utils
{
    public class Messages
    {
        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void SuccessMessage(string message)
        {
            MessageBox.Show(message, "Mensaje de información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ConfirmMessage(string message)
        {
            return MessageBox.Show(message, "Mensaje de confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}