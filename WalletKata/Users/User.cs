using System.Collections;
using System.Collections.Generic;
using WalletKata.Wallets;

namespace WalletKata.Users
{
    public class User
    {
        private List<User> friends = new List<User>();
        private List<Wallet> wallets = new List<Wallet>();

        public IEnumerable GetFriends()
        {
            return friends;
        }

        public void AddFriend(User friend)
        {
            friends.Add(friend);
        }

        public bool HasFriend(User friend)
        {
            return friends.Contains(friend);
        }

        public void AddWallet(Wallet wallet)
        {
            wallets.Add(wallet);
        }

        public List<Wallet> GetWallets()
        {
            return wallets;
        }
    }
}