namespace mind_your_money_domain;

public class Report
{
    public int Id { get; set; }
    public List<User> Users { get; set; }
    public List<Expense> Expenses { get; set; }

    public void addUser(User user)
    {
        //TODO: Validate for duplicates.
        Users.Add(user);
    }

    public void addExpense(Expense expense)
    {
        Expenses.Add(expense);
    }
}