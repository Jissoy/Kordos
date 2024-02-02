// Student.cs
public class Student
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Wydzial { get; set; }
    public string Kierunek { get; set; }
    public string Grupa { get; set; }
    public List<string> Grupy { get; set; } = new List<string>();
}
