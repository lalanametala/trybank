namespace trybank;

public class Trybank
{
    public bool Logged;
    public int loggedUser;
    
    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;
    public Trybank()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    public void RegisterAccount(int number, int agency, int pass)
    {
        try
        {
            for(int i = 0; i < maxAccounts; i++)
            {
                if(Bank[i,0] == number && Bank[i,1] == agency ) throw new ArgumentException("A conta já está sendo usada!");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }

        Bank[registeredAccounts, 0] = number;
        Bank[registeredAccounts, 1] = agency;
        Bank[registeredAccounts, 2] = pass;
        Bank[registeredAccounts, 3] = 0;
        registeredAccounts++;
    }

    public bool IsAccountValid(int line, int number, int agency)
    {
        return Bank[line, 0] == number && Bank[line, 1] == agency;
    }

    public bool IsPasswordValid(int line, int pass)
    {
        return Bank[line, 2] == pass;
    }

    public void Login(int number, int agency, int pass)
    {
        if(Logged) throw new AccessViolationException("Usuário já está logado");
        for(int i = 0; i < maxAccounts; i++)
        {
            if(IsAccountValid(i, number, agency))
            {
                if(IsPasswordValid(i, pass))
                {
                    Logged = true;
                    loggedUser = i;
                    break;
                }
                else {
                    throw new ArgumentException("Senha incorreta");
                }
            }
        }
        if(!Logged) throw new ArgumentException("Agência + Conta não encontrada");
    }

    public void IsUserLogged()
    {
        if(!Logged) throw new AccessViolationException("Usuário não está logado");
    }

    public void Logout()
    {
        IsUserLogged();

        Logged = false;
        loggedUser = -99;
    }

    public int CheckBalance()
    {
        IsUserLogged();

        return Bank[loggedUser, 3];  
    }

    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        IsUserLogged();

        int newBalance = Bank[loggedUser, 3] - value;

        if (newBalance < 0) throw new InvalidOperationException("Saldo insuficiente");

        for(int i = 0; i < maxAccounts; i++)
        {
            if(Bank[i,0] == destinationNumber && Bank[i,1] == destinationAgency) 
            {
                Bank[i,3] += value;
                Bank[loggedUser, 3] = newBalance;
            }
        }
}

    public void Deposit(int value)
    {
        IsUserLogged();
        Bank[loggedUser, 3] += value;
    }

    public void Withdraw(int value)
    {
        IsUserLogged();

        int newBalance = Bank[loggedUser, 3] - value;

        if (newBalance < 0) throw new InvalidOperationException("Saldo insuficiente");

        Bank[loggedUser, 3] = newBalance;
    }
}
