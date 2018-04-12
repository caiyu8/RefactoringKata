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
            ValidateLogIn(loggedUser);

            List<Wallet> walletList = new List<Wallet>();

           return user.HasFriend(loggedUser) ? walletDAO.FindWalletsByUser(user) : new List<Wallet>();
        }

        private void ValidateLogIn(User loggedUser)
        {
            if (null == loggedUser)
            {
                throw new UserNotLoggedInException();
            }
        }
    }
    public interface IWalletDAO
    {
        List<Wallet> FindWalletsByUser(User user);
    }
}