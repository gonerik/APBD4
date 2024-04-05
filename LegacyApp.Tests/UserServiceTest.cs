using System;
using JetBrains.Annotations;
using LegacyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LegacyApp.Tests;

[TestClass]
[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    private readonly UserService _userService;
    public UserServiceTest()
    {
        _userService = new UserService();
    }

    [TestMethod]
    public void AddUser_Should_Return_False_When_First_Name_Is_Incorrect()
    {
        var addUser = _userService.AddUser("", "Garrot", "ivan.r@gmail.com", DateTime.Parse("1982-03-03"),1);
        Assert.IsFalse(addUser);
    }
    [TestMethod]
    public void AddUser_Should_Return_False_When_Second_Name_Is_Incorrect()
    {
        var addUser = _userService.AddUser("John", "", "ivan.r@gmail.com", DateTime.Parse("1982-03-03"),1);
        Assert.IsFalse(addUser);
    }
    [TestMethod]
    public void AddUser_Should_Return_False_when_Email_Is_Incorrect()
    {
        var addUser = _userService.AddUser("John", "Doe", "ivanrgmailcom", DateTime.Parse("1982-03-03"),1);
        Assert.IsFalse(addUser);
    }
    [TestMethod]
    public void AddUser_Should_Throw_Exception_When_cleintID_Doesnt_Exists()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            _userService.AddUser("John", "Doe", "ivanr@gmail.com", DateTime.Parse("1982-03-03"), -1);
        });
    }
    [TestMethod]
    public void AddUser_Should_Return_False_When_Age_Is_Under_21()
    {
        var addUser = _userService.AddUser("John", "Doe", "ivanr@gmail.com", DateTime.Parse("2012-03-03"), 1);
        Assert.IsFalse(addUser);
    }
    [TestMethod]
    public void AddUser_Should_Throw_Exception_When_There_Is_No_Such_LastName_In_ClientCredit_Service()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            _userService.AddUser("John", "Andrzejewicz", "ivanr@gmail.com", DateTime.Parse("1982-03-03"), 6);
        });
    }
    [TestMethod]
    public void AddUser_Should_Return_False_When_User_Has_Credit_Limit_And_Its_Lower_Than_500()
    {
        var addUser = _userService.AddUser("John", "Kowalski", "ivanr@gmail.com", DateTime.Parse("1982-03-03"), 1);
        Assert.IsFalse(addUser);
    }
    [TestMethod]
    public void AddUser_Should_Return_True()
    {
        var addUser = _userService.AddUser("John", "Doe", "ivanr@gmail.com", DateTime.Parse("1982-03-03"), 1);
        Assert.IsTrue(addUser);
    }
    [TestMethod]
    public void AddUser_Should_Throw_Exception_When_There_Is_No_Such_LastName_In_ClientCredit_Service_And_The_Client_Is_Important()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            _userService.AddUser("John", "Andrzejewicz", "ivanr@gmail.com", DateTime.Parse("1982-03-03"), 3);
        });
    }
}