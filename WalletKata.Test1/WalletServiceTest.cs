using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WalletKata.Exceptions;
using WalletKata.Users;
using WalletKata.Wallets;
using Moq;

namespace WalletKata.Test1
{
    [TestClass]
    public class WalletServiceTest
    {
        static readonly User unLoggedUser = null;
        static readonly User userA = new User();
        static readonly User loggedUser = new User();

        Mock<IWalletDAO> walletDAOMock = new Mock<IWalletDAO>();
        WalletService walletService;

        [TestInitialize]
        public void SetUp()
        {
            walletService = new WalletService(walletDAOMock.Object);
        }

        [TestMethod()]
        public void ShouldThrowUserNotLoggedInExceptionIfUserNotLoggedIn()
        {
            Assert.ThrowsException<UserNotLoggedInException>(() => walletService.GetWalletsByUser(userA, unLoggedUser));
        }

        [TestMethod()]
        public void ShouldNotReturnWalletIfLoggedUserIsNotFriend()
        {
            
            List<Wallet> walletList = walletService.GetWalletsByUser(userA, loggedUser);

            Assert.AreEqual(0, walletList.Count);
        }

        [TestMethod()]
        public void ShouldReturnWalletIfLoggedUserIsFriend()
        {
            userA.AddFriend(loggedUser);
            userA.AddWallet(new Wallet());

            walletDAOMock.Setup(x => x.FindWalletsByUser(userA)).Returns(userA.GetWallets());

            List<Wallet> walletList = walletService.GetWalletsByUser(userA, loggedUser);

            CollectionAssert.AreEqual(walletList, userA.GetWallets());

            walletDAOMock.VerifyAll();
        }
    }
}
