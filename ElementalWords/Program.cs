using ElementalWords.Services;

Console.WriteLine("== Elemental Words ==\n");
Console.WriteLine("Enter words to find their elemental forms or 'exit' to quit:\n");

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        break;
    }

    var results = ElementWordFinder.ElementalForms(input);

    if (results.Length == 0)
    {
        Console.WriteLine("  No elemental forms found.\n");
    }
    else
    {
        Console.WriteLine($"  Found {results.Length} elemental form(s):");
        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {string.Join(" + ", results[i])}");
        }
        Console.WriteLine();
    }
}

Console.WriteLine("Many thanks");
