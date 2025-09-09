namespace School.AdminService.Helpers
{
    public static class StaticHelpers
    {
        private static long _counter = 0;

        public static long GeneratId()
        {
            // Get Unix time in milliseconds (13 digits)
            var milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // Use a thread-safe counter for uniqueness within the same millisecond
            var counter = Interlocked.Increment(ref _counter) & 0x3FF; // Limit to 10 bits (0–1023)

            // Combine: use lower 42 bits of timestamp + 10 bits counter = fits in 52 bits
            // But for simplicity, just concatenate as number
            return milliseconds * 1000 + counter; // Adds up to 3 extra digits
        }
    }
}
