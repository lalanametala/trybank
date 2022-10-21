using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestFirstReq
{
    [Theory(DisplayName = "Deve cadastrar contas com sucesso!")]
    [InlineData(111, 111, 777)]
    public void TestRegisterAccountSucess(int number, int agency, int pass)
    {        
        Trybank instance = new();
        instance.RegisterAccount(number, agency, pass);
        instance.registeredAccounts.Should().Be(1);
        instance.Bank[0,0].Should().Be(number);
        instance.Bank[0,1].Should().Be(agency);
        instance.Bank[0,2].Should().Be(pass);
    }

    [Theory(DisplayName = "Deve retornar ArgumentException ao cadastrar contas que já existem")]
    [InlineData(111, 111, 777)]
    public void TestRegisterAccountException(int number, int agency, int pass)
    {        
        Trybank instance = new();
        instance.RegisterAccount(number, agency, pass);
        Action act = () => instance.RegisterAccount(number, agency, pass);
        act.Should().Throw<ArgumentException>().WithMessage("A conta já está sendo usada!");
    }
}