// Program.cs
using System;

class Program
{
    static void Main()
    {
        Uczelnia uczelnia = new Uczelnia();

        uczelnia.StudentDodany += (sender, student) => Console.WriteLine($"Dodano nowego studenta: {student.Imie} {student.Nazwisko}");
        uczelnia.StudentUsuniety += (sender, student) => Console.WriteLine($"Usunięto studenta: {student.Imie} {student.Nazwisko}");

        while (true)
        {
            Console.WriteLine("1. Dodaj studenta");
            Console.WriteLine("2. Usuń studenta");
            Console.WriteLine("3. Wyszukaj studenta");
            Console.WriteLine("4. Edytuj dane uczelni");
            Console.WriteLine("5. Sprawdź listę studentów");
            Console.WriteLine("0. Wyjdź");

            int wybor = Convert.ToInt32(Console.ReadLine());

            switch (wybor)
            {
                case 1:
                    Console.Write("Imię: ");
                    string imie = Console.ReadLine();

                    Console.Write("Nazwisko: ");
                    string nazwisko = Console.ReadLine();

                    Console.WriteLine("Dostępne wydziały:");
                    for (int i = 0; i < uczelnia.PobierzListeWydzialow().Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {uczelnia.PobierzListeWydzialow()[i]}");
                    }
                    Console.Write("Wybierz numer wydziału: ");
                    int numerWydzialu = Convert.ToInt32(Console.ReadLine()) - 1;
                    string wydzial = uczelnia.PobierzListeWydzialow()[numerWydzialu];

                    Console.WriteLine("Dostępne kierunki:");
                    for (int i = 0; i < uczelnia.PobierzListeKierunkow().Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {uczelnia.PobierzListeKierunkow()[i]}");
                    }
                    Console.Write("Wybierz numer kierunku: ");
                    int numerKierunku = Convert.ToInt32(Console.ReadLine()) - 1;
                    string kierunek = uczelnia.PobierzListeKierunkow()[numerKierunku];

                    Console.Write("Grupa: ");
                    string grupa = Console.ReadLine();

                    uczelnia.DodajStudenta(imie, nazwisko, wydzial, kierunek, grupa);
                    break;

                case 2:
                    Console.Write("Imię studenta do usunięcia: ");
                    string imieDoUsuniecia = Console.ReadLine();

                    Console.Write("Nazwisko studenta do usunięcia: ");
                    string nazwiskoDoUsuniecia = Console.ReadLine();

                    uczelnia.UsunStudenta(imieDoUsuniecia, nazwiskoDoUsuniecia);
                    break;

                case 3:
                    Console.Write("Imię studenta do wyszukania: ");
                    string imieDoWyszukania = Console.ReadLine();

                    Console.Write("Nazwisko studenta do wyszukania: ");
                    string nazwiskoDoWyszukania = Console.ReadLine();

                    Student znalezionyStudent = uczelnia.WyszukajStudenta(imieDoWyszukania, nazwiskoDoWyszukania);

                    if (znalezionyStudent != null)
                    {
                        Console.WriteLine($"Znaleziono studenta: {znalezionyStudent.Imie} {znalezionyStudent.Nazwisko}");
                    }
                    else
                    {
                        Console.WriteLine("Nie znaleziono studenta.");
                    }
                    break;

                case 4:
                    uczelnia.EdytujDaneUczelni();
                    break;

                case 5:
                    List<Student> listaStudentow = uczelnia.PobierzListeStudentow();

                    Console.WriteLine("Lista studentów:");
                    foreach (var student in listaStudentow)
                    {
                        Console.WriteLine($"{student.Imie} {student.Nazwisko} - {student.Wydzial}, {student.Kierunek}, {string.Join(", ", student.Grupy)}");

                    }
                    break;

                case 0:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }
}
