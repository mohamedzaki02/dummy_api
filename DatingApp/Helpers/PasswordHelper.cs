namespace DatingApp.Helpers
{
    public static class PasswordHelper
    {


        public static void Compute(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public static bool Verify(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computed_pass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computed_pass.Length; i++)
                {
                    if (computed_pass[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }





    }


}