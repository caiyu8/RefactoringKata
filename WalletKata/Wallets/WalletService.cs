using System.Collections.Generic;
using WalletKata.Users;
using WalletKata.Exceptions;
using System;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        public List<Wallet> GetWalletsByUser(User user)
        {
            User loggedUser = GetLoggedUser();
            CheckUser(loggedUser); // Verify if user is logged in before continuing

            List<Wallet> walletList = new List<Wallet>();

            //bool isFriend = false;

            //foreach (User friend in user.GetFriends())
            //{
            //    if (friend.Equals(loggedUser))
            //    {
            //        isFriend = true;
            //        break;
            //    }
            //}

            //if (isFriend)
            //{
            //    walletList = new List<Wallet>();
            //}

            return user.HasFriend(loggedUser) ? FindWalletByUser(user) : new List<Wallet>();
        }

        private void CheckUser(User loggedUser)
        {
            if (null == loggedUser)
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