using AutoBogus;
using AutoBogus.NSubstitute;

namespace PersonCRUD.Tests;

public class BaseTests
{
    public BaseTests() =>
      AutoFaker.Configure(builder => builder
        .WithLocale("pt_BR")
        .WithBinder<NSubstituteBinder>());
}