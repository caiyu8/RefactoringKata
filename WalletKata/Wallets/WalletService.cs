using System.Collections.Generic;
using WalletKata.Users;
using WalletKata.Exceptions;
using System;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        private IWalletDAO walletDAO;
        public WalletService(IWalletDAO walletDAO) => this.walletDAO = walletDAO;
        public List<Wallet> GetWalletsByUser(User user, User loggedUser)
        {
            ValidateUsers(loggedUser, user);

            List<Wallet> walletList = new List<Wallet>();

           return user.HasFriend(loggedUser) ? walletDAO.FindWalletsByUser(user) : new List<Wallet>();
        }

        private void ValidateUsers(User loggedUser, User user)
        {
            if (null == loggedUser)
            {
                throw new UserNotLoggedInException();
            }

            if(null == user)
            {
                throw new ArgumentNullException();
            }
        }
    }
    public interface IWalletDAO
    {
        List<Wallet> FindWalletsByUser(User user);
    }
}