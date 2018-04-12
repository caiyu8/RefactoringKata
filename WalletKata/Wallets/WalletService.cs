using System.Collections.Generic;
using WalletKata.Users;
using WalletKata.Exceptions;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        public List<Wallet> GetWalletsByUser(User user)
        {
            List<Wallet> walletList = new List<Wallet>();
            User loggedUser = GetLoggedUser();
            bool isFriend = false;

            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    walletList = FindWalletByUser(user);
                }

                return walletList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        
        protected virtual List<Wallet> FindWalletByUser(User user)
        {
            return WalletDAO.FindWalletsByUser(user);
        }

        /// <summary>
        /// Extract this method in order to make code testable
        /// </summary>
        /// <returns></returns>
        protected virtual User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}