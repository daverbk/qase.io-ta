using Bogus;
using DiplomaProject.Models;

namespace DiplomaProject.Fakers;

public class DefectFaker : Faker<Defect>
{
    private const int LowSeverity = 6;
    private const int HighSeverity = 1;

    public DefectFaker()
    {
        RuleFor(project => project.Title, faker => faker.Commerce.ProductName());
        RuleFor(project => project.Severity, faker => faker.Random.Int(HighSeverity, LowSeverity).ToString());
        RuleFor(project => project.ActualResult, faker => faker.Company.CatchPhrase());
    }
}
