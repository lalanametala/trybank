using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestFourthReq
{
    [Theory(DisplayName = "Deve transefir um valor com uma conta logada")]
    [InlineData(20, 10)]
    public void TestTransferSucess(int balance, int value)
    {        
        Trybank instance = new() {
            Logged = true,
            loggedUser = 0,
        };
        instance.RegisterAccount(11, 11, 11);
        instance.RegisterAccount(22, 22, 22);
        instance.Bank[0,3] = balance;
        instance.Transfer(22, 22, value);
        instance.CheckBalance().Should().Be(balance - value);
        instance.Bank[1,3].Should().Be(value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestTransferWithoutLogin(int value)
    {        
        Trybank instance = new();
        Action act = () => instance.Transfer(1, 1, value);
        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(10, 20)]
    public void TestTransferWithoutBalance(int balance, int value)
    {        
        Trybank instance = new() {
            Logged = true,
            loggedUser = 0,
        };
        instance.Bank[0,3] = balance;
        Action act = () => instance.Transfer(1, 1, value);
        act.Should().Throw<InvalidOperationException>().WithMessage("Saldo insuficiente");
    }
}
