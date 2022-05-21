using Bogus;
using DiplomaProject.Models;

namespace DiplomaProject.Fakers;

public class DefectFaker : Faker<Defect>
{
    private const int MinSeverity = 6;
    private const int MaxSeverity = 1;

    public DefectFaker()
    {
        RuleFor(project => project.Title, faker => faker.Commerce.ProductName());
        RuleFor(project => project.Severity, faker => faker.Random.Int(MaxSeverity, MinSeverity).ToString());
        RuleFor(project => project.ActualResult, faker => faker.Company.CatchPhrase());
    }
}
