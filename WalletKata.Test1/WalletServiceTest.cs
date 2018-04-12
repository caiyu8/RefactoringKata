using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WalletKata.Exceptions;
using WalletKata.Users;
using WalletKata.Wallets;

namespace WalletKata.Test1
{
    [TestClass]
    public class WalletServiceTest
    {
        WalletService walletService = new FakeWalletService();

        static readonly User unLoggedUser = null;
        static User userA = new User();
        static User loggedUser = new User();

        [TestMethod()]
        public void ShouldThrowUserNotLoggedInExceptionIfUserNotLoggedIn()
        {
            loggedUser = unLoggedUser;
            Assert.ThrowsException<UserNotLoggedInException>(() => walletService.GetWalletsByUser(userA));
        }

        [TestMethod()]
        public void ShouldNotReturnWalletIfLoggedUserIsNotFriend()
        {
            List<Wallet> walletList = walletService.GetWalletsByUser(userA);

            Assert.AreEqual(0, walletList.Count);
        }

        [TestMethod()]
        public void ShouldReturnWalletIfLoggedUserIsFriend()
        {
            userA.AddFriend(loggedUser);
            userA.AddWallet(new Wallet());

            List<Wallet> walletList = walletService.GetWalletsByUser(userA);

            CollectionAssert.AreEqual(walletList, userA.GetWallets());
        }

        class FakeWalletService : WalletService
        {
            protected override User GetLoggedUser()
            {
                return loggedUser;
            }

            protected override List<Wallet> FindWalletByUser(User user)
            {
                return user.GetWallets();
            }
        }
    }
}
