using System;

namespace IDI.Core.Common
{
    public sealed class SecretKey
    {
        public string Hash { get; private set; }

        public string Salt { get; private set; }

        public SecretKey(string secret)
        {
            if (secret.IsNull())
                throw new ArgumentNullException("secret");

            var entries = secret.Split('.', StringSplitOptions.RemoveEmptyEntries);

            this.Hash = entries[0];
            this.Salt = entries[1];
        }

        public SecretKey(string password, string salt)
        {
            this.Hash = Cryptography.Encrypt(password, salt);
            this.Salt = salt;
        }

        public bool Verify(string password)
        {
            return this.Hash.Equals(Cryptography.Encrypt(password, this.Salt), StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            return $"{this.Hash}.{this.Salt}";
        }
    }
}
