using Bogus;
using DiplomaProject.Models;

namespace DiplomaProject.Fakers;

public class TestCaseFaker : Faker<TestCase>
{
    public TestCaseFaker()
    {
        RuleFor(project => project.Title, faker => faker.Commerce.ProductName());
        RuleFor(project => project.Description, faker => faker.Company.CatchPhrase());
        RuleFor(project => project.Preconditions, faker => faker.Company.CatchPhrase());
        RuleFor(project => project.Postconditions, faker => faker.Company.CatchPhrase());
    }
}
