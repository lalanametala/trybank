using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestSecondReq
{
    [Theory(DisplayName = "Deve logar em uma conta!")]
    [InlineData(7, 8, 9)]
    public void TestLoginSucess(int number, int agency, int pass)
    {        
        Trybank instance = new();
        instance.RegisterAccount(number, agency, pass);
        instance.Login(number, agency, pass);
        instance.Logged.Should().BeTrue();
    }

    [Theory(DisplayName = "Deve retornar exceção ao tentar logar em conta já logada")]
    [InlineData(0, 0, 0)]
    public void TestLoginExceptionLogged(int number, int agency, int pass)
    {        
        Trybank instance = new() {
            Logged = true
        };
        Action act = () => instance.Login(number, agency, pass);
        act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve retornar exceção ao errar a senha")]
    [InlineData(0, 0, 0)]
    public void TestLoginExceptionWrongPass(int number, int agency, int pass)
    {        
        throw new NotImplementedException();
    }

    [Theory(DisplayName = "Deve retornar exceção ao digitar conta que não existe")]
    [InlineData(0, 0, 0)]
    public void TestLoginExceptionNotFounded(int number, int agency, int pass)
    {        
        throw new NotImplementedException();
    }

    [Theory(DisplayName = "Deve sair de uma conta!")]
    [InlineData(0, 0, 0)]
    public void TestLogoutSucess(int number, int agency, int pass)
    {        
        throw new NotImplementedException();
    }

    [Theory(DisplayName = "Deve retornar exceção ao sair quando não está logado")]
    [InlineData(0, 0, 0)]
    public void TestLogoutExceptionNotLogged(int number, int agency, int pass)
    {        
        throw new NotImplementedException();
    }

}
