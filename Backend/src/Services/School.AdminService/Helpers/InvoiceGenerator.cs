namespace School.AdminService.Helpers
{
    public static class InvoiceGenerator
    {
        private static readonly char[] Letters = "BCDFGHJKLMNPQRSTVWXYZ".ToCharArray(); // Consonants only (no vowels)
        private static readonly Random Random = new Random();

        public static string GenerateInvoiceNo()
        {
            // Generate 2 random letters
            var letter1 = Letters[Random.Next(Letters.Length)];
            var letter2 = Letters[Random.Next(Letters.Length)];

            // Generate 6-digit number (000000 to 999999)
            var numberPart = Random.Next(0, 1000000).ToString("D6"); // Zero-padded to 6 digits

            return $"{letter1}{letter2}{numberPart}";
        }
    }
}
