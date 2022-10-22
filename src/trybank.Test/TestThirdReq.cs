using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestThirdReq
{
    [Theory(DisplayName = "Deve devolver o saldo em uma conta logada")]
    [InlineData(0)]
    public void TestCheckBalanceSucess(int balance)
    {        
        Trybank instance = new() {
            Logged = true,
            loggedUser = 0,
        };
        instance.Bank[0,3] = balance;            
        instance.CheckBalance().Should().Be(balance);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestCheckBalanceWithoutLogin(int balance)
    {        
        Trybank instance = new();
        Action act = () => instance.CheckBalance();
        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve depositar um saldo em uma conta logada")]
    [InlineData(10)]
    public void TestDepositSucess(int value)
    {        
        Trybank instance = new() {
            Logged = true,
            loggedUser = 0,
        };
        instance.Bank[0,3] = 10;
        instance.Deposit(value);
        instance.CheckBalance().Should().Be(value + 10);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestDepositWithoutLogin(int value)
    {        
        Trybank instance = new();
        Action act = () => instance.Deposit(value);
        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve sacar um valor em uma conta logada")]
    [InlineData(0, 0)]
    public void TestWithdrawSucess(int balance, int value)
    {        
        throw new NotImplementedException();
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestWithdrawWithoutLogin(int value)
    {        
        throw new NotImplementedException();
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0, 0)]
    public void TestWithdrawWithoutBalance(int balance, int value)
    {        
        throw new NotImplementedException();
    }
}